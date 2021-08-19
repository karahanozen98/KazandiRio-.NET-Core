using KazandiRio.Application.Helpers;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KazandiRio.Application.Services
{
    public interface IAuthService
    {
        public Task<RefreshToken> GenerateJwtToken(User user, string ipAddress);
        public Task<RefreshToken> GetRefreshTokenAsync(string tokenString);
    }

    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDBContext _db;

        public AuthService(IOptions<AppSettings> appSettings, ApplicationDBContext context)
        {
            _appSettings = appSettings.Value;
            _db = context;
        }

        public async Task<RefreshToken> GenerateJwtToken(User user, string ipAddress)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var createdAt = DateTime.UtcNow;
            var expiresAt = createdAt.AddDays(30);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            RefreshToken refreshToken = new RefreshToken
            {
                Created = createdAt,
                CreatedByIp = ipAddress,
                Expires = expiresAt,
                Token = tokenHandler.WriteToken(token)
            };
            _db.RefreshToken.Add(refreshToken);
            await _db.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string tokenString)
        {
            var refreshToken = await _db.RefreshToken.FirstOrDefaultAsync(x => x.Token == tokenString);

            // if token doesn't exist
            if (refreshToken == null)
                return null;

            // Token is valid 
            if (!refreshToken.IsExpired && refreshToken.IsActive)
            {
                // Update Expire Date
                refreshToken.Expires = DateTime.UtcNow.AddDays(30);
                _db.RefreshToken.Update(refreshToken);
                await _db.SaveChangesAsync();
                // return Token
                return refreshToken;
            }
            // Token is invalid!
            else
                return null;

        }
    }
}
