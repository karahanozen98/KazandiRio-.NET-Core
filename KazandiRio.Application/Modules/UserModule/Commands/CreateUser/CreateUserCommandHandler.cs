using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.UserModule.Commands.CreateUser
{
    class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Boolean>
    {
        private readonly ApplicationDBContext dbContext;

        public CreateUserCommandHandler(ApplicationDBContext db)
        {
            dbContext = db;
        }

        public async Task<Boolean> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await dbContext.User.FirstOrDefaultAsync(m => m.Username == request.User.Username);

            if (checkUser != null)
            {
                throw new Exception("Kullanici adi zaten alinmis");
            }

            dbContext.User.Add(request.User);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
