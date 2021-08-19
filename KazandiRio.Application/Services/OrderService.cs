using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazandiRio.Application.Services
{
    public interface IOrderService
    {
        public Task<List<ProductDto>> GetMyOrdersAsync(string token, IUserService userService);
        //public Task<Boolean> BuyProductWithBalanceAsync(OrderDto order);
        //public Task<Boolean> BuyProductWithRewardsAsync(OrderDto order);
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationDBContext _db;
        public OrderService(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<List<ProductDto>> GetMyOrdersAsync(string token, IUserService userService)
        {
            List<ProductDto> products = new List<ProductDto>();

            UserDto user = await userService.GetUserByTokenAsync(token);
           // User user = await _db.User.FirstOrDefaultAsync(user => user.Id == u.Id);
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

        //public async Task<Boolean> BuyProductWithBalanceAsync(OrderDto order)
        //{
        //    User user = await _db.User.FirstOrDefaultAsync(user => user.Id == order.UserId);
        //    Product product = await _db.Product.FirstOrDefaultAsync(product => product.Id == order.ProductList);
        //    IsOrderValid(user, product);

        //    if (user.Balance >= product.Price)
        //    {
        //        user.Balance -= product.Price;
        //        _db.Order.Add(new Order { UserId = user.Id, ProductId = product.Id });
        //        _db.User.Update(user);
        //        await _db.SaveChangesAsync();
        //        return true;
        //    }
        //    else
        //        throw new Exception("Insufficient balance");

        //}

        //public async Task<Boolean> BuyProductWithRewardsAsync(OrderDto order)
        //{
        //    User user = await _db.User.FirstOrDefaultAsync(user => user.Id == order.UserId);
        //    Product product = await _db.Product.FirstOrDefaultAsync(product => product.Id == order.ProductId);
        //    IsOrderValid(user, product);

        //    if (user.Rewards >= product.Price)
        //    {
        //        user.Rewards -= product.Price;
        //        _db.Order.Add(new Order { UserId = user.Id, ProductId = product.Id });
        //        _db.User.Update(user);
        //        await _db.SaveChangesAsync();
        //        return true;
        //    }
        //    else if (user.Rewards < product.Price && user.Balance + user.Rewards >= product.Price)
        //    {
        //        var diff = product.Price - user.Rewards;
        //        user.Rewards = 0;
        //        user.Balance -= diff;
        //        _db.Order.Add(new Order { UserId = user.Id, ProductId = product.Id });
        //        _db.User.Update(user);
        //        await _db.SaveChangesAsync();
        //        return true;
        //    }
        //    else
        //        throw new Exception("Insufficient balance");
        //}

        private void IsOrderValid(User user, Product product)
        {
            if (user == null)
                throw new Exception("User is not exist");
            else if (product == null)
                throw new Exception("Product is not exist");
        }
    }
}
