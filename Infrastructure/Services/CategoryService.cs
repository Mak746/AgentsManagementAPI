using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<Category> AddCategory(Category entity)
        {
            try
            {
                _unitOfWork.Repository<Category>().Add(entity);
                var result = await _unitOfWork.Complete();
                if (result <= 0) return null;
                return entity;
            }
            catch (Exception ex)
            { 
              throw;
            }

        }

        public async Task<Category> DeleteCategory(Guid id)
        {
            try
            {
                var result = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
                _unitOfWork.Repository<Category>().Delete(result);
                var deletedFromDb = await _unitOfWork.Complete();
                if (deletedFromDb <= 0) return null;
                return result;
            }
            catch (Exception ex)
            { 
              throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
                return result;
            }
            catch (Exception ex)
            { 
              throw;
            }

        }

        public async Task<IReadOnlyList<Category>> ListAllCategoryAsync()
        {
            try
            {
                var result = await _unitOfWork.Repository<Category>().ListAllAsync();
                return result;
            }
            catch (Exception ex)
            { 
              throw;
            }

        }

        public async Task<Category> UpdateCategory(Category entity)
        {
            try
            {
                _unitOfWork.Repository<Category>().Update(entity);
                var result = await _unitOfWork.Complete();
                if (result <= 0) return null;
                return entity;
            }
            catch (Exception ex)
            { 
              throw;
            }

        }

    }
}