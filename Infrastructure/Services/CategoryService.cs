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
            _unitOfWork.Repository<Category>().Add(entity);
            // TODO: save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            // return agent
            return entity;
        }

        public async Task<Category> DeleteCategory(Category entity)
        {
            _unitOfWork.Repository<Category>().Delete(entity);
            // TODO: save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            // return agent
            return entity;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var result = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            return result;
        }

        public async Task<IReadOnlyList<Category>> ListAllCategoryAsync()
        {
            var result = await _unitOfWork.Repository<Category>().ListAllAsync();
            // TODO: save to db
            // var result = await _unitOfWork.Complete();

            // if (result <= 0) return null;
            // return agent
            return result;
        }

        public async Task<Category> UpdateCategory(Category entity)
        {
            _unitOfWork.Repository<Category>().Update(entity);
            // TODO: save to db
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;
            // return agent
            return entity;
        }

    }
}