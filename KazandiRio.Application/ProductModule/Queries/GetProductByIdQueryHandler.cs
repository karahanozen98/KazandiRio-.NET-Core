using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.ProductModule.Queries
{
    class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly ApplicationDBContext _db;

        public GetProductByIdQueryHandler(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await _db.Product.FirstOrDefaultAsync(m => m.Id == request.Id);
            Category category = null;

            if (product.CategoryId != null || product.CategoryId != 0)
                category = await _db.Category.FirstOrDefaultAsync(x => x.Id == product.CategoryId);

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
