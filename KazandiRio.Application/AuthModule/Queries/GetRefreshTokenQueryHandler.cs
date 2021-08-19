using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.AuthModule.Queries
{
    class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, RefreshToken>
    {
        private readonly ApplicationDBContext _db;

        public GetRefreshTokenQueryHandler(ApplicationDBContext context)
        {
            _db = context;
        }
        public async Task<RefreshToken> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
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
                // return Token
                return refreshToken;
            }
            // Token is invalid!
            else
                return null;
        }
    }
}
