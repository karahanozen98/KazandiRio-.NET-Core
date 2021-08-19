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

namespace KazandiRio.Application.UserModule.Queries
{
    class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly ApplicationDBContext _db;
        
        public GetAllUsersQueryHandler(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _db.User.ToListAsync();
            return users.Select(x => new UserDto { Id = x.Id, Username = x.Username, Role = x.Role, Balance = x.Balance, Rewards = x.Rewards });
        }
    }
}
