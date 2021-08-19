using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.UserModule.Commands
{
    public class CreateUserCommand: IRequest<Boolean>
    {
        public User User { get; set; }
    }
}
