using KazandiRio.Core.Exceptions;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.ProductModule.Commands.UpdateProduct
{
    class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Boolean>
    {
        private readonly ApplicationDBContext dbContext;

        public UpdateProductCommandHandler(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Product.FirstOrDefaultAsync(product => product.Id == request.Product.Id);

            if (product == null)
            {
                throw new NotFoundException("Urun bulunamadi");
            }

            product.Name = request.Product.Name;
            product.Price = request.Product.Price;
            product.CategoryId = request.Product.CategoryId;
            product.ImageUrl = request.Product.ImageUrl;

            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
