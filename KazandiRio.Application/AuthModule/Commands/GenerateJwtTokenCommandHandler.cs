using KazandiRio.Application.AuthModule.Commands;
using KazandiRio.Application.DTO;
using KazandiRio.Application.Helpers;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.AuthModule.Commands
{
    class GenerateJwtTokenCommandHandler : IRequestHandler<GenerateJwtTokenCommand, RefreshToken>
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDBContext _db;

        public GenerateJwtTokenCommandHandler(IOptions<AppSettings> appSettings, ApplicationDBContext context)
        {
            _appSettings = appSettings.Value;
            _db = context;
        }

        public async Task<RefreshToken> Handle(GenerateJwtTokenCommand request, CancellationToken cancellationToken)
        {
            User user = request.User;
            string ipAddress = request.IpAdress;

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
    }
}
