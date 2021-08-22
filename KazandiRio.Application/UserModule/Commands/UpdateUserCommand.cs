using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.UserModule.Commands
{
    public class UpdateUserCommand : IRequest<Boolean>
    {
        public UserDto User { get; set; }
    }
}
