using KazandiRio.Application.AuthModule.Queries;
using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.UserModule.Queries
{
    class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, UserDto>
    {

        private readonly ApplicationDBContext _db;
        private readonly IMediator _mediatr;

        public GetUserByTokenQueryHandler(IMediator mediator, ApplicationDBContext db)
        {
            _mediatr = mediator;
            _db = db;
        }

        public async Task<UserDto> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            //RefreshToken refreshToken = await authService.GetRefreshTokenAsync(tokenString);
            RefreshToken refreshToken = await _mediatr.Send(new GetRefreshTokenQuery { TokenString = request.TokenString });

            if (refreshToken == null)
                return null;

            var user = await _db.User.FirstOrDefaultAsync(user => user.TokenId == refreshToken.Id);
            if (user != null)
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role,
                    Balance = user.Balance,
                    Rewards = user.Rewards,
                    Token = refreshToken.Token
                };
            else
                return null;
        }
    }
}
