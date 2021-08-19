using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.UserModule.Queries
{
    public class GetUserByUsernameAndPasswordCommand : IRequest<UserDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }

    }
}
