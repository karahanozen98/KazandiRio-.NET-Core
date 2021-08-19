using KazandiRio.Application.DTO;
using KazandiRio.Application.Services;
using KazandiRio.Application.UserModule.Commands;
using KazandiRio.Application.UserModule.Queries;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KazandiRio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediatr;

        public object Roles { get; private set; }

        public UsersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }


        // GET: All Users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediatr.Send(new GetAllUsersQuery());
            return Json(users);
        }


        // GET: User by username and password
        [HttpPost("login")]
        public async Task<IActionResult> MakeLoginRequestWithUsernameAndPasswordAsync(LoginDto loginInfo)
        {
            GetUserByUsernameAndPasswordCommand query = new GetUserByUsernameAndPasswordCommand
            {
                Username = loginInfo.Username,
                Password = loginInfo.Password,
                IpAddress = ipAddress()
            };
            var user = await _mediatr.Send(query);

            if (user == null)
                return Json("Kullanıcı adı veya şifre yanlış");
            else
                return Json(user);
        }

        // GET: User by token
        [HttpPost("authorize")]
        public async Task<IActionResult> MakeLoginRequestWithTokenAsync(TokenDto token)
        {
            //var user = await _userService.GetUserByTokenAsync(token.Token);
            var user = await _mediatr.Send(new GetUserByTokenQuery { TokenString = token.Token });

            if (user == null)
                return Json("Invalid token!");
            else
                return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoginDto u)
        {
            User user = new User { Username = u.Username, Password = u.Password, Role = Role.Consumer, Balance = 0, Rewards = 0 };
             await _mediatr.Send(new CreateUserCommand { User = user });
             return Json("Ok");
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
