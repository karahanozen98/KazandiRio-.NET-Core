using KazandiRio.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.AuthModule.Commands
{
    public class GenerateJwtTokenCommand : IRequest<RefreshToken>
    {
        public User User { get; set; }
        public string IpAdress { get; set; }
    }
}
