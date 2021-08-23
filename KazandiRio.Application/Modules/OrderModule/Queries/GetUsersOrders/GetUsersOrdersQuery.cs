using KazandiRio.Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace KazandiRio.Application.Modules.OrderModule.Queries.GetUsersOrders
{
    public class GetUsersOrdersQuery : IRequest<IEnumerable<ProductDto>>
    {
        public int UserId { get; set; }
    }
}
