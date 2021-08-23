using KazandiRio.Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace KazandiRio.Application.Modules.UserModule.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {

    }
}
