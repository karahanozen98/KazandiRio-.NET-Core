using KazandiRio.Domain.Entities;
using MediatR;

namespace KazandiRio.Application.Modules.AuthModule.Queries.GetRefreshToken
{
    public class GetRefreshTokenQuery : IRequest<RefreshToken>
    {
        public string TokenString { get; set; }
    }
}
