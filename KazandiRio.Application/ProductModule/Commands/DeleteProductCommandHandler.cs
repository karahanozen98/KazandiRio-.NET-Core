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
    class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;

        public DeleteProductCommandHandler(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Product.FirstOrDefaultAsync(prod => prod.Id == request.ProductId);
            if (product == null) throw new Exception("Urun bulunamadi");
            _db.Product.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
