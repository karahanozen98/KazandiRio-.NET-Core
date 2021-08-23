using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.ProductModule.Commands.CreateProduct
{
    class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;

        public CreateProductCommandHandler(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Category isCategoryExist = await _db.Category.FirstOrDefaultAsync(c => c.Id == request.Product.CategoryId);

            // Unknown category type
            if (request.Product.CategoryId != null && isCategoryExist == null)
                throw new Exception("Kategori bulunamadı");

            else
            {
                _db.Product.Add(new Product
                {
                    Name = request.Product.Name,
                    Price = request.Product.Price,
                    CategoryId = request.Product.CategoryId,
                    ImageUrl = request.Product.ImageUrl
                });
                await _db.SaveChangesAsync();
                return true;
            }
        }
    }
}
