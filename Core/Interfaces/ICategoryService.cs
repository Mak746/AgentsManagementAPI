using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IReadOnlyList<Category>> ListAllCategoryAsync();
        Task<Category> AddCategory(Category entity);
        Task<Category> UpdateCategory(Category entity);
        Task<Category> DeleteCategory(int id);
    }
}