using KazandiRio.Application.DTO;
using KazandiRio.Application.OrderModule.Commands;
using KazandiRio.Application.OrderModule.Queries;
using KazandiRio.Application.Services;
using KazandiRio.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KazandiRio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = Role.Admin+","+Role.Consumer)]
    public class OrdersController : Controller
    {
       private readonly IMediator _mediatr;

        public OrdersController(IMediator mediatr, IOrderService service, IUserService userService)
        {
            _mediatr = mediatr;
        }

        [HttpPost("myorders")]
        public async Task<ActionResult> GetMyOrdersAsync(UserIdDto userIdDto)
        {
            var orders = await _mediatr.Send(new GetUsersOrdersQuery { UserId = userIdDto.userId });
            return Json(orders);
        }

        [HttpPost("balance")]
        public async Task<ActionResult> BuyProductWithBalance([FromBody] OrderDto order)
        {
            await _mediatr.Send(new CreateOrderByBalanceCommand { Order = order });
            return Json("Ok");

        }

        [HttpPost("rewards")]
        public async Task<ActionResult> BuyProductWithRewards([FromBody] OrderDto order)
        {
            await _mediatr.Send(new CreateOrderByRewardsCommand { Order = order });
            return Json("Ok");

        }
    }
}
