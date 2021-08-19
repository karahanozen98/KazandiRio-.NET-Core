using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.UserModule.Commands
{
    class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;

        public CreateUserCommandHandler(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<Boolean> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var checkUser = await _db.User.FirstOrDefaultAsync(m => m.Username == request.User.Username);
            if (checkUser != null)
                throw new Exception("Kullanici adi zaten alinmis");

            _db.User.Add(request.User);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
