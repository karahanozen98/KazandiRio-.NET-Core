using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.AuthModule.Queries.GetRefreshToken
{
    class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, RefreshToken>
    {
        private readonly ApplicationDBContext dbContext;

        public GetRefreshTokenQueryHandler(ApplicationDBContext context)
        {
            dbContext = context;
        }
        public async Task<RefreshToken> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await dbContext.RefreshToken.FirstOrDefaultAsync(x => x.Token == request.TokenString);

            if (refreshToken == null)
            {
                return null;
            }
            if (!refreshToken.IsExpired && refreshToken.IsActive)
            {
                refreshToken.Expires = DateTime.UtcNow.AddDays(30);
                dbContext.RefreshToken.Update(refreshToken);
                await dbContext.SaveChangesAsync();

                return refreshToken;
            }
            else
            {
                return null;
            }
        }
    }
}
