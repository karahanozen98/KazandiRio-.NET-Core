using KazandiRio.Repository.DAL;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.CategoryModule.Commands.CreateCategory
{
    class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Boolean>
    {
        private readonly ApplicationDBContext dbContext;

        public CreateCategoryCommandHandler(ApplicationDBContext db)
        {
            dbContext = db;
        }
        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            dbContext.Category.Add(request.Category);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
