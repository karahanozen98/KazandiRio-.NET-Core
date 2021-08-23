using KazandiRio.Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace KazandiRio.Application.Modules.ProductModule.Queries.GetAllProducts
{
    public class GetAllProductsQuery: IRequest<IEnumerable<ProductDto>>
    {
    }
}
