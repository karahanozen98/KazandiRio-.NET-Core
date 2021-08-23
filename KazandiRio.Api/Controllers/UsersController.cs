using KazandiRio.Application.DTO;
using KazandiRio.Application.Modules.UserModule.Commands.CreateUser;
using KazandiRio.Application.Modules.UserModule.Commands.UpdateBalance;
using KazandiRio.Application.Modules.UserModule.Commands.UpdateUser;
using KazandiRio.Application.Modules.UserModule.Queries.GetAllUsers;
using KazandiRio.Application.Modules.UserModule.Queries.GetUserByToken;
using KazandiRio.Application.Modules.UserModule.Queries.GetUserByUsernameAndPassword;
using KazandiRio.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KazandiRio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediatr;

        public UsersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        // GET: All Users
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediatr.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        // GET: User by username and password
        [HttpPost("login")]
        public async Task<IActionResult> MakeLoginRequestWithUsernameAndPasswordAsync(LoginDto loginInfo)
        {
            var query = new GetUserByUsernameAndPasswordCommand(loginInfo.Username, loginInfo.Password, ipAddress());
            var user = await _mediatr.Send(query);
            return Ok(user);
        }

        // GET: User by token
        [HttpPost("authorize")]
        public async Task<IActionResult> MakeLoginRequestWithTokenAsync(TokenDto token)
        {
            var user = await _mediatr.Send(new GetUserByTokenQuery { TokenString = token.Token });
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoginDto u)
        {
            var user = new User { Username = u.Username, Password = u.Password, Role = Role.Consumer, Balance = 0, Rewards = 0 };
            await _mediatr.Send(new CreateUserCommand { User = user });
            return Ok("Ok");
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Update(UserDto u)
        {
            await _mediatr.Send(new UpdateUserCommand { User = u });
            return Ok("Ok");
        }

        [HttpPut("deposit")]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Deposit(DepositDto deposit)
        {
            await _mediatr.Send(new UpdateBalanceCommand { UserId = deposit.UserId, Amount = deposit.Amount });
            return Ok("Ok");
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
