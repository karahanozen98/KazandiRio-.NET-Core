using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.ProductModule.Queries.GetProductById
{
    class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly ApplicationDBContext dbContext;

        public GetProductByIdQueryHandler(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Product.FirstOrDefaultAsync(m => m.Id == request.Id);
            Category category = null;

            if (product.CategoryId != null || product.CategoryId != 0)
            {
                category = await dbContext.Category.FirstOrDefaultAsync(x => x.Id == product.CategoryId);
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = category,
                ImageUrl = product.ImageUrl
            };
        }
    }
}
