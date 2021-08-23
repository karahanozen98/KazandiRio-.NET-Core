using KazandiRio.Application.DTO;
using KazandiRio.Core.Exceptions;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.CategoryModule.Queries.GetCategoryById
{
    class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ApplicationDBContext dbContext;

        public GetCategoryByIdQueryHandler(ApplicationDBContext db)
        {
            dbContext = db;
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await dbContext.Category.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (category == null)
            {
                throw new NotFoundException("Kategori bulunamadı");
            }

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                RewardAmount = category.RewardAmount
            };
        }
    }
}
