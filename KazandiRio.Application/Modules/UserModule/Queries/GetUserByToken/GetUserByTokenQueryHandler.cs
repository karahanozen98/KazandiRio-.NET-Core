using KazandiRio.Application.DTO;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.UserModule.Queries.GetUserByToken
{
    class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQuery, UserDto>
    {

        private readonly ApplicationDBContext _db;

        public GetUserByTokenQueryHandler(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<UserDto> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _db.RefreshToken.FirstOrDefaultAsync(x => x.Token == request.TokenString);

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
            }
            // Token is invalid!
            else
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
