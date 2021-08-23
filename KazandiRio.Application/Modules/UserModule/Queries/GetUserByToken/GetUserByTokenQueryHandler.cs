using KazandiRio.Application.DTO;
using KazandiRio.Core.Exceptions;
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

        private readonly ApplicationDBContext dbContext;

        public GetUserByTokenQueryHandler(ApplicationDBContext db)
        {
            dbContext = db;
        }

        public async Task<UserDto> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.RefreshToken.FirstOrDefaultAsync(x => x.Token == request.TokenString);

            if (refreshToken == null)
            {
                throw new NotFoundException("Token bulunamadı");
            }
            if (!refreshToken.IsExpired && refreshToken.IsActive)
            {
                refreshToken.Expires = DateTime.UtcNow.AddDays(30);
                dbContext.RefreshToken.Update(refreshToken);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException("Token geçersiz");
            }

            var user = await dbContext.User.FirstOrDefaultAsync(user => user.TokenId == refreshToken.Id);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı");
            }

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
