using KazandiRio.Application.DTO;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace KazandiRio.Application.Modules.CategoryModule.Queries.GetAllCategories
{
    class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ApplicationDBContext dbContext;

        public GetAllCategoriesQueryHandler(ApplicationDBContext db)
        {
            dbContext = db;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await dbContext.Category.ToListAsync();
            return categories.Select(x => new CategoryDto { Id = x.Id, Name = x.Name, RewardAmount = x.RewardAmount });
        }
    }
}
