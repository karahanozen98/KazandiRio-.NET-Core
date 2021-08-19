using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.ProductModule.Commands
{
    class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;

        public UpdateProductCommandHandler(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var isProductExist = await _db.Product.FirstOrDefaultAsync(product => product.Id == request.Product.Id);

            if (isProductExist == null)
                throw new Exception("Product not found");

            _db.Product.Update(request.Product);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
