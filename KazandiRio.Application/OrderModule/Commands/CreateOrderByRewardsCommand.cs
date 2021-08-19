using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.OrderModule.Commands
{
    public class CreateOrderByRewardsCommand: IRequest<Boolean>
    {
        public OrderDto Order { get; set; }
    }
}
