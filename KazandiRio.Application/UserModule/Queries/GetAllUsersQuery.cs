using KazandiRio.Application.DTO;
using KazandiRio.Repository.DAL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.UserModule.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
 
    }
}
