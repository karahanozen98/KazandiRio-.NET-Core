using KazandiRio.Application.DTO;
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
        private readonly ApplicationDBContext _db;
        public GetUsersOrdersQueryHandler(IMediator mediatr, ApplicationDBContext context)
        {
            _mediatr = mediatr;
            _db = context;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetUsersOrdersQuery request, CancellationToken cancellationToken)
        {
            List<ProductDto> products = new List<ProductDto>();

            // Get user by id
            User user = await _db.User.FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (user == null)
                return null;

            var orders = _db.Order.Where(order => order.UserId == user.Id);

            orders.ToList().ForEach(async order =>
            {
                Product p = await _db.Product.FirstOrDefaultAsync(p => p.Id == order.ProductId);
                Category c = null;

                if (p != null)
                    if (p.CategoryId != null || p.CategoryId != 0)
                        c = await _db.Category.FirstOrDefaultAsync(category => category.Id == p.CategoryId);

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
