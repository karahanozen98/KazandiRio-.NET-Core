using KazandiRio.Application.DTO;
using KazandiRio.Core.Exceptions;
using KazandiRio.Core.Helpers;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.UserModule.Queries.GetUserByUsernameAndPassword
{
    class GetUserByUsernameAndPasswordQueryHandler : IRequestHandler<GetUserByUsernameAndPasswordCommand, UserDto>
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDBContext dbContext;

        public GetUserByUsernameAndPasswordQueryHandler(IOptions<AppSettings> appSettings, ApplicationDBContext context)
        {
            _appSettings = appSettings.Value;
            dbContext = context;
        }
        public async Task<UserDto> Handle(GetUserByUsernameAndPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.User.FirstOrDefaultAsync(m => m.Username == request.Username && m.Password == request.Password);

            if (user == null)
            {
                throw new NotFoundException("Wrong username or password");
            }

            var refreshToken = GenerateJwtToken(user, request.IpAddress, _appSettings.Secret);
            dbContext.RefreshToken.Add(refreshToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            user.TokenId = refreshToken.Id;
            dbContext.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                Balance = user.Balance,
                Rewards = user.Rewards,
                Token = refreshToken.Token,
            };
        }
        private RefreshToken GenerateJwtToken(User user, string ipAddress, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
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
            var refreshToken = new RefreshToken
            {
                Created = createdAt,
                CreatedByIp = ipAddress,
                Expires = expiresAt,
                Token = tokenHandler.WriteToken(token)
            };

            return refreshToken;
        }
    }
}
