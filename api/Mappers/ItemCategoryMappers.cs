using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ItemCategory;
using api.Models;

namespace api.Mappers
{
    public static class ItemCategoryMappers
    {
        public static ItemCategoryDto ToItemCategoryDto(this ItemCategory itemCategoryModel)
        {
            return new ItemCategoryDto
            {
                Item = itemCategoryModel.Item,
                Category = itemCategoryModel.Category,
                ItemId = itemCategoryModel.ItemId,
                CategoryId = itemCategoryModel.CategoryId
            };
        } 

        public static ItemCategory ToItemCategoryFromCreate(this CreateItemCategoryDto itemCategoryDto)
        {
            return new ItemCategory
            {
                ItemId = itemCategoryDto.ItemId,
                CategoryId = itemCategoryDto.CategoryId,
                //Item = itemCategoryDto.Item,
                //Category = itemCategoryDto.Category
            };
        }      
    }
}