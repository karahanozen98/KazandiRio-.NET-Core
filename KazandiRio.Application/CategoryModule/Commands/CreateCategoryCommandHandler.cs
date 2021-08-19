using KazandiRio.Repository.DAL;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.CategoryModule.Commands
{
    class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;

        public CreateCategoryCommandHandler(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            _db.Category.Add(request.Category);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
