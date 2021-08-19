using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.ProductModule.Queries
{
    public class GetAllProductsQuery: IRequest<IEnumerable<ProductDto>>
    {
    }
}
