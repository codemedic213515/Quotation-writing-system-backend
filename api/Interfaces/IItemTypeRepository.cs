using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IItemTypeRepository
    {
        Task<List<ItemType>> GetAllAsync();    
        Task<ItemType?> GetByIdAsync(int id);
        Task<ItemType> CreateAsync(ItemType ItemTypeModel);
        Task<ItemType?> UpdateAsync(int id, ItemType ItemTypeModel);
        Task<ItemType?> DeleteAsync(int id);    
    }
}