using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.UserModule.Queries
{
    public class GetUserByTokenQuery: IRequest<UserDto>
    {
        public string TokenString { get; set; }
    }
}
