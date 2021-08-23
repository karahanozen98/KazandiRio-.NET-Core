using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KazandiRio.Application.Modules.OrderModule.Commands.CreateOrderByRewards
{
    class CreateOrderByRewardsCommandHandler : IRequestHandler<CreateOrderByRewardsCommand, Boolean>
    {
        private readonly ApplicationDBContext _db;
        public CreateOrderByRewardsCommandHandler(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<bool> Handle(CreateOrderByRewardsCommand request, CancellationToken cancellationToken)
        {
            User user = await _db.User.FirstOrDefaultAsync(user => user.Id == request.Order.UserId);
            float totalPrice = 0;
            float totalRewards = 0;

            if (user == null)
                throw new Exception("Kullanıcı bulunamdı");

            for (int i = 0; i < request.Order.ProductList.Length; i++)
            {
                Product product = await _db.Product.FirstOrDefaultAsync(product => product.Id == request.Order.ProductList[i]);
                Category category = null;

                if (product == null)
                    throw new Exception("Ürün bulunamadı");

                if (product.CategoryId > 0)
                    category = await _db.Category.FirstOrDefaultAsync(category => category.Id == product.CategoryId);

                totalPrice += product.Price;
                if (category != null) totalRewards += category.RewardAmount;

            }


            if (user.Rewards >= totalPrice)
            {
                for (int i = 0; i < request.Order.ProductList.Length; i++)
                {
                    _db.Order.Add(new Order { UserId = user.Id, ProductId = request.Order.ProductList[i] });
                }
                user.Rewards -= totalPrice;
                user.Rewards += totalRewards;

                _db.User.Update(user);
                await _db.SaveChangesAsync();
                return true;
            }

            else
                throw new Exception("Yetersiz bakiye");
        }
    }
}

