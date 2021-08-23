using KazandiRio.Application.DTO;
using KazandiRio.Core.Exceptions;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.OrderModule.Queries.GetUsersOrders
{
    class GetUsersOrdersQueryHandler : IRequestHandler<GetUsersOrdersQuery, IEnumerable<ProductDto>>
    {
        private readonly IMediator _mediatr;
        private readonly ApplicationDBContext dbContext;
        public GetUsersOrdersQueryHandler(IMediator mediatr, ApplicationDBContext context)
        {
            _mediatr = mediatr;
            dbContext = context;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetUsersOrdersQuery request, CancellationToken cancellationToken)
        {
            var products = new List<ProductDto>();
            var user = await dbContext.User.FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunmadı");
            }

            var orders = dbContext.Order.Where(order => order.UserId == user.Id);

            orders.ToList().ForEach(async order =>
            {
                var p = await dbContext.Product.FirstOrDefaultAsync(p => p.Id == order.ProductId);
                Category c = null;

                if (p != null)
                {
                    if (p.CategoryId != null || p.CategoryId != 0)
                    {
                        c = await dbContext.Category.FirstOrDefaultAsync(category => category.Id == p.CategoryId);
                    }
                }

                products.Add(new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    Category = c,
                    ImageUrl = p.ImageUrl
                });

            });
            return products;
        }
    }
}
