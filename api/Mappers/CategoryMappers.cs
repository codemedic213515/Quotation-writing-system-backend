using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Category;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Parent = categoryModel.Parent,
                Description = categoryModel.Description,
                IsLeaf = categoryModel.IsLeaf
            };
        }

        public static Category ToCategoryFromCreate(this CreateCategoryDto categoryDto)
        {
            return new Category
            {
                Description = categoryDto.Description,
                IsLeaf = categoryDto.IsLeaf,
            };
        }

        public static Category ToCategoryFromUpdate(this UpdateCategoryRequestDto categoryDto)
        {
            return new Category
            {
                ParentId = categoryDto.ParentId,
                Description = categoryDto.Description,
                IsLeaf = categoryDto.IsLeaf,
            };
        }
    }
}
