using KazandiRio.Application.DTO;
using MediatR;
using System.Collections.Generic;

namespace KazandiRio.Application.Modules.CategoryModule.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
    }
}
