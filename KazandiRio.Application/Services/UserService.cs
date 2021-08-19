using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using KazandiRio.Application.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using KazandiRio.Application.Helpers;

namespace KazandiRio.Application.Services
{

    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllUsersAsync();
        public Task<UserDto> MakeLoginRequestWithUsernameAndPasswordAsync(string username, string password, string ipAddress);
        public Task<UserDto> GetUserByTokenAsync(string tokenString);
        public Task<Boolean> CreateUserAsync(User user);

    }

    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _db;
        private readonly AuthService authService;

        public UserService(IOptions<AppSettings> appSettings, ApplicationDBContext context)
        {
            authService = new AuthService(appSettings, context);
            _db = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _db.User.ToListAsync();
            return users.Select(x => new UserDto { Id = x.Id, Username = x.Username, Role = x.Role, Balance = x.Balance, Rewards = x.Rewards });
        }

        public async Task<UserDto> MakeLoginRequestWithUsernameAndPasswordAsync(string username, string password, string ipAddress)
        {
            var user = await _db.User.FirstOrDefaultAsync(m => m.Username == username && m.Password == password);
            if (user == null)
                return null;

            // Authentication && create JWT token.
            RefreshToken refreshToken = await authService.GenerateJwtToken(user, ipAddress);
            user.TokenId = refreshToken.Id; // assign new token to the user
            _db.Update(user);  
            await _db.SaveChangesAsync();

            return ConvertUserToUserDto(user, refreshToken);
        }

        // Post 
        public async Task<UserDto> GetUserByTokenAsync(string tokenString)
        {
            RefreshToken refreshToken = await authService.GetRefreshTokenAsync(tokenString);

            if (refreshToken == null)
                return null;

            var user = await _db.User.FirstOrDefaultAsync(user => user.TokenId == refreshToken.Id);
            if (user != null)
                return ConvertUserToUserDto(user, refreshToken);
            else
                return null;

        }

        // PUT 
        public async Task<Boolean> CreateUserAsync(User user)
        {
            var checkUser = await _db.User.FirstOrDefaultAsync(m => m.Username == user.Username);
            if (checkUser != null)
                return false;

            _db.User.Add(user);
            await _db.SaveChangesAsync();
            return true;
        }

        private UserDto ConvertUserToUserDto(User user, RefreshToken refreshToken)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                Balance = user.Balance,
                Rewards = user.Rewards,
                Token = refreshToken.Token
            };
        }
    }
}
