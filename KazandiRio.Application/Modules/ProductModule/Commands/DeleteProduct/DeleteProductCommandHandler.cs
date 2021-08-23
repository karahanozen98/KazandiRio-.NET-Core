using KazandiRio.Core.Exceptions;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.ProductModule.Commands.DeleteProduct
{
    class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Boolean>
    {
        private readonly ApplicationDBContext dbContext;

        public DeleteProductCommandHandler(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Product.FirstOrDefaultAsync(prod => prod.Id == request.ProductId);

            if (product == null)
            {
                throw new NotFoundException("Urun bulunamadi");
            }

            dbContext.Product.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
