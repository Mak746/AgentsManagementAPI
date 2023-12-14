using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            try
            {
                var categoriesList = await _categoryService.ListAllCategoryAsync();
       
                return Ok(categoriesList);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSingleCategory(int id)
        {
            try
            {
                var singleCategory = await _categoryService.GetCategoryByIdAsync(id);
                var categoryDto = _mapper.Map<CategoryDto>(singleCategory);
                if (categoryDto == null) return NotFound(new ApiResponse(404));
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AgentsDto>> CreateCategoryAsync(CategoryDto category)
        {
            try
            {
                var fromDto = _mapper.Map<Category>(category);
                var result = await _categoryService.AddCategory(fromDto);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }


        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AgentsDto>> UpdateCategoryAsync(CategoryDto category)
        {
            try
            {
                var singleCategory = await _categoryService.GetCategoryByIdAsync(category.Id);
                var categoryDto = _mapper.Map<CategoryDto>(singleCategory);

                if (categoryDto == null) return NotFound(new ApiResponse(404));
                var fromDto = _mapper.Map<Category>(category);
                var result = await _categoryService.UpdateCategory(fromDto);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }


        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            try
            {
                var singleCategory = await _categoryService.GetCategoryByIdAsync(id);
                var categoryDto = _mapper.Map<CategoryDto>(singleCategory);
                if (categoryDto == null) return NotFound(new ApiResponse(404));

                var deletedCategory = await _categoryService.DeleteCategory(id);
                if (deletedCategory == null) return false;
                return true;
            }
            catch (Exception ex)
            {
                return StatusCode(HttpContext.Response.StatusCode, "Internal server error");
            }

        }
    }

}