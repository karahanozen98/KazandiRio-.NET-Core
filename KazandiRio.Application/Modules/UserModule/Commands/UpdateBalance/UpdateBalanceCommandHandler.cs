using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.UserModule.Commands.UpdateBalance
{
    class UpdateBalanceCommandHandler : IRequestHandler<UpdateBalanceCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;

        public UpdateBalanceCommandHandler(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(UpdateBalanceCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.User.FirstOrDefaultAsync(u => u.Id == request.UserId);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");
            if (request.Amount <= 0) throw new Exception("Miktar sıfırdan büyük olmalı");
            user.Balance += request.Amount;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
