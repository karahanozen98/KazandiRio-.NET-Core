using KazandiRio.Application.DTO;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.UserModule.Queries.GetAllUsers
{
    class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly ApplicationDBContext dbContext;

        public GetAllUsersQueryHandler(ApplicationDBContext db)
        {
            dbContext = db;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await dbContext.User.ToListAsync();
            return users.Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.Username,
                Role = x.Role,
                Balance = x.Balance,
                Rewards = x.Rewards
            });
        }
    }
}
