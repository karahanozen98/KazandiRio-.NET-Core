using System;
using System.Collections.Generic;
using System.Text;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using KazandiRio.Application.DTO;

namespace KazandiRio.Application.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProductsAsync();
        public Task<ProductDto> GetProductByIdAsync(int id);
        public Task<Boolean> CreateProductAsync(Product product);
        public Task<Boolean> UpdateProductAsync(Product newProduct);
    }
    public class ProductService : IProductService
    {
        private readonly ApplicationDBContext _db;

        public ProductService(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            List<Product> products = await _db.Product.ToListAsync();
            List<ProductDto> productsWithCategoryInfo = new List<ProductDto>();

            products.ToList().ForEach(async product =>
            {
                Category c = null;
                if (product.CategoryId != null || product.CategoryId != 0)
                    c = await _db.Category.FirstOrDefaultAsync(x => x.Id == product.CategoryId);

                productsWithCategoryInfo.Add(new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    Category = c,
                    ImageUrl = product.ImageUrl
                   
                });
            });

            return productsWithCategoryInfo;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            Product product = await _db.Product.FirstOrDefaultAsync(m => m.Id == id);
            Category category = null;

            if (product.CategoryId != null || product.CategoryId != 0)
                category = await _db.Category.FirstOrDefaultAsync(x => x.Id == product.CategoryId);

            return new ProductDto {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = category,
                ImageUrl = product.ImageUrl
            };
        }

        public async Task<Boolean> CreateProductAsync(Product product)
        {
            Category isCategoryExist = await _db.Category.FirstOrDefaultAsync(c => c.Id == product.CategoryId);

            // Unknown category type
            if (product.CategoryId != null && isCategoryExist == null)      
                throw new Exception("Category not found");
            
            else
            {
                _db.Product.Add(product);
                await _db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Boolean> UpdateProductAsync(Product newProduct)
        {
            var oldProduct = await GetProductByIdAsync(newProduct.Id);
            if (oldProduct == null)
                return false;
            else
            {
                _db.Product.Update(newProduct);
                await _db.SaveChangesAsync();
                return true;
            }
        }
    }
}
