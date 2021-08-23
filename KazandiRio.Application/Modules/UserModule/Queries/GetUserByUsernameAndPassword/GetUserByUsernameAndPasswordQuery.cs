using KazandiRio.Application.DTO;
using MediatR;

namespace KazandiRio.Application.Modules.UserModule.Queries.GetUserByUsernameAndPassword
{
    public class GetUserByUsernameAndPasswordCommand : IRequest<UserDto>
    {
        public GetUserByUsernameAndPasswordCommand(string username, string password, string ipAddress)
        {
            Username = username;
            Password = password;
            IpAddress = ipAddress;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }

    }
}
