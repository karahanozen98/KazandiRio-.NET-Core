using KazandiRio.Application.DTO;
using MediatR;
using System;

namespace KazandiRio.Application.Modules.OrderModule.Commands.CreateOrderByRewards
{
    public class CreateOrderByRewardsCommand : IRequest<Boolean>
    {
        public OrderDto Order { get; set; }
    }
}
