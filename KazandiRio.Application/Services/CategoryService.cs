using KazandiRio.Application.DTO;
using KazandiRio.Domain.Entities;
using KazandiRio.Repository.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KazandiRio.Application.Services
{
    public interface ICategoryService{
        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        public Task<CategoryDto> GetCateogryByIdAsync(int id);
        public Task<Boolean> CreateCategoryAsync(Category category);
        public Task<Boolean> UpdateCategoryAsync(Category newCategory);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDBContext _db;

        public CategoryService(ApplicationDBContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _db.Category.ToListAsync();
            return categories.Select(x => new CategoryDto { Id = x.Id, Name = x.Name, RewardAmount = x.RewardAmount });
        }

        public async Task<CategoryDto> GetCateogryByIdAsync(int id)
        {
            var category = await _db.Category.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
                return null;

            return new CategoryDto { Id = category.Id, Name = category.Name, RewardAmount = category.RewardAmount };
        }

        public async Task<Boolean> CreateCategoryAsync(Category category)
        {
            _db.Category.Add(category);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Boolean> UpdateCategoryAsync(Category newCategory)
        {
            var oldCategory = await GetCateogryByIdAsync(newCategory.Id);
            if (oldCategory == null)
                return false;
            else
            {
                _db.Category.Update(newCategory);
                await _db.SaveChangesAsync();
                return true;
            }
            
        }
    }
}
