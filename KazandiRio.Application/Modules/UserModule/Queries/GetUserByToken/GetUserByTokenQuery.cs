using KazandiRio.Application.DTO;
using MediatR;

namespace KazandiRio.Application.Modules.UserModule.Queries.GetUserByToken
{
    public class GetUserByTokenQuery : IRequest<UserDto>
    {
        public string TokenString { get; set; }
    }
}
