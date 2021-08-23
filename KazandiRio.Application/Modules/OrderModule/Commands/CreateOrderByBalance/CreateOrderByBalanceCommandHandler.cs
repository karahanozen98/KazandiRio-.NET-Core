using KazandiRio.Core.Exceptions;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.OrderModule.Commands.CreateOrderByBalance
{
    class CreateOrderByBalanceCommandHandler : IRequestHandler<CreateOrderByBalanceCommand, Boolean>
    {
        private readonly ApplicationDBContext dbContext;
        public CreateOrderByBalanceCommandHandler(ApplicationDBContext context)
        {
            dbContext = context;
        }

        public async Task<bool> Handle(CreateOrderByBalanceCommand request, CancellationToken cancellationToken)
        {
            var user = await dbContext.User.FirstOrDefaultAsync(user => user.Id == request.Order.UserId);
            float totalPrice = 0;
            float totalRewards = 0;

            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamdı");
            }

            for (int i = 0; i < request.Order.ProductList.Length; i++)
            {
                var product = await dbContext.Product.FirstOrDefaultAsync(product => product.Id == request.Order.ProductList[i]);
                Category category = null;

                if (product == null)
                {
                    throw new NotFoundException("Ürün bulunamadı");
                }

                if (product.CategoryId > 0)
                    category = await dbContext.Category.FirstOrDefaultAsync(category => category.Id == product.CategoryId);

                totalPrice += product.Price;
                if (category != null) totalRewards += category.RewardAmount;

            }

            if (user.Balance >= totalPrice)
            {
                for (int i = 0; i < request.Order.ProductList.Length; i++)
                {
                    dbContext.Order.Add(new Order { UserId = user.Id, ProductId = request.Order.ProductList[i] });
                }
                user.Balance -= totalPrice;
                user.Rewards += totalRewards;
                dbContext.User.Update(user);
                await dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            else
            {
                throw new Exception("Yetersiz bakiye");
            }
        }
    }
}
