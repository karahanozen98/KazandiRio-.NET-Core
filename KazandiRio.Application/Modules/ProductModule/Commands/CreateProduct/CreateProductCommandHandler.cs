using KazandiRio.Core.Exceptions;
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
        private readonly ApplicationDBContext dbContext;

        public CreateProductCommandHandler(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await dbContext.Category.FirstOrDefaultAsync(c => c.Id == request.Product.CategoryId);

            if (request.Product.CategoryId != null && category == null)
            {
                throw new NotFoundException("Kategori bulunamadı");
            }
            else
            {
                dbContext.Product.Add(new Product
                {
                    Name = request.Product.Name,
                    Price = request.Product.Price,
                    CategoryId = request.Product.CategoryId,
                    ImageUrl = request.Product.ImageUrl
                });
                await dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
