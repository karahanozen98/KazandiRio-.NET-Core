using KazandiRio.Domain.Entities;
using MediatR;
using System;

namespace KazandiRio.Application.Modules.UserModule.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Boolean>
    {
        public User User { get; set; }
    }
}
