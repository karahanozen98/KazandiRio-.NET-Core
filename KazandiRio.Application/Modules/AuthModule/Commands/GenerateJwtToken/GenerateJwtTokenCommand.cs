using KazandiRio.Domain.Entities;
using MediatR;

namespace KazandiRio.Application.Modules.AuthModule.Commands.GenerateJwtToken
{
    public class GenerateJwtTokenCommand : IRequest<RefreshToken>
    {
        public User User { get; set; }
        public string IpAdress { get; set; }
    }
}
