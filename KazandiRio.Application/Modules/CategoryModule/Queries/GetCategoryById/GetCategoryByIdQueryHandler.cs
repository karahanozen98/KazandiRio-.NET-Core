using KazandiRio.Application.DTO;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.CategoryModule.Queries.GetCategoryById
{
    class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ApplicationDBContext _db;

        public GetCategoryByIdQueryHandler(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _db.Category.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (category == null)
                return null;

            return new CategoryDto { Id = category.Id, Name = category.Name, RewardAmount = category.RewardAmount };
        }
    }
}
