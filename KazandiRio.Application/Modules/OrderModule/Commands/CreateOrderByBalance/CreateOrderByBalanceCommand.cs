using KazandiRio.Application.DTO;
using MediatR;
using System;

namespace KazandiRio.Application.Modules.OrderModule.Commands.CreateOrderByBalance
{
    public class CreateOrderByBalanceCommand : IRequest<Boolean>
    {
        public OrderDto Order { get; set; }
    }
}
