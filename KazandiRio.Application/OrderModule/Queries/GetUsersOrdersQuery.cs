using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.OrderModule.Queries
{
    public class GetUsersOrdersQuery: IRequest<IEnumerable<ProductDto>>
    {
        public int UserId { get; set; }
    }
}
