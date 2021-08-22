using KazandiRio.Application.DTO;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.UserModule.Commands
{
    class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;

        public UpdateUserCommandHandler(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.User.FirstOrDefaultAsync(u => u.Id == request.User.Id);
            if (user == null) throw new Exception("Kullanııcı bulunamadı");
            if(request.User.Role != Role.Admin && request.User.Role != Role.Admin) throw new Exception("Hatalı rol bilgisi");
            if(request.User.Balance < 0 || request.User.Rewards <0) throw new Exception("Bakiye bilgisi sıfırdan küçük olamaz");

            user.Role = request.User.Role;
            user.Balance = request.User.Balance;
            user.Rewards = request.User.Rewards;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
