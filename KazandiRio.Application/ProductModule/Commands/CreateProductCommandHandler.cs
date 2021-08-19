using KazandiRio.Domain.Entities;
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
    class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, Boolean>
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
                _db.Product.Add(request.Product);
                await _db.SaveChangesAsync();
                return true;
            }
        }
    }
}
