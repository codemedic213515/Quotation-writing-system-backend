using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IItemCategoryRepository
    {
        Task<List<ItemCategory>> GetAllAsync(ItemCategoryQueryObject query); 
        Task<ItemCategory?> GetByIdAsync(int itemId, int categoryId);
        Task<ItemCategory> CreateAsync(ItemCategory itemCategoryModel);
        Task<ItemCategory?> DeleteAsync(int itemId, int categoryId);     
    }
}