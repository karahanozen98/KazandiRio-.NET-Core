using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.ProductModule.Queries.GetAllProducts
{
    class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly ApplicationDBContext _db;

        public GetAllProductsQueryHandler(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _db.Product.ToListAsync();
            List<ProductDto> productsWithCategoryInfo = new List<ProductDto>();

            products.ToList().ForEach(async product =>
            {
                Category c = null;
                if (product.CategoryId != null || product.CategoryId != 0)
                    c = await _db.Category.FirstOrDefaultAsync(x => x.Id == product.CategoryId);

                productsWithCategoryInfo.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    Category = c,
                    ImageUrl = product.ImageUrl

                });
            });

            return productsWithCategoryInfo;
        }
    }
}
