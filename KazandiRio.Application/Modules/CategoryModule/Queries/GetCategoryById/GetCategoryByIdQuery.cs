using KazandiRio.Application.DTO;
using MediatR;

namespace KazandiRio.Application.Modules.CategoryModule.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}
