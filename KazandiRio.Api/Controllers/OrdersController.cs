using KazandiRio.Application.DTO;
using KazandiRio.Application.Modules.OrderModule.Commands.CreateOrderByBalance;
using KazandiRio.Application.Modules.OrderModule.Commands.CreateOrderByRewards;
using KazandiRio.Application.Modules.OrderModule.Queries.GetUsersOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KazandiRio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediatr;

        public OrdersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost("myorders")]
        public async Task<ActionResult> GetMyOrdersAsync(UserIdDto userIdDto)
        {
            var orders = await _mediatr.Send(new GetUsersOrdersQuery { UserId = userIdDto.userId });
            return Ok(orders);
        }

        [HttpPost("balance")]
        public async Task<ActionResult> BuyProductWithBalance([FromBody] OrderDto order)
        {
            await _mediatr.Send(new CreateOrderByBalanceCommand { Order = order });
            return Ok("Ok");
        }

        [HttpPost("rewards")]
        public async Task<ActionResult> BuyProductWithRewards([FromBody] OrderDto order)
        {
            await _mediatr.Send(new CreateOrderByRewardsCommand { Order = order });
            return Ok("Ok");
        }
    }
}
