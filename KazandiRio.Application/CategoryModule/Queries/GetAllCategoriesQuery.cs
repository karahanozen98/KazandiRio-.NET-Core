using KazandiRio.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KazandiRio.Application.CategoryModule.Queries
{
    public class GetAllCategoriesQuery: IRequest<IEnumerable<CategoryDto>>
    {
    }
}
