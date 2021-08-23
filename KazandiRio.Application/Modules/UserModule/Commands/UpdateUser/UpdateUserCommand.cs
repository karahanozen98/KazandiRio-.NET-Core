using KazandiRio.Application.DTO;
using MediatR;
using System;

namespace KazandiRio.Application.Modules.UserModule.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<Boolean>
    {
        public UserDto User { get; set; }
    }
}
