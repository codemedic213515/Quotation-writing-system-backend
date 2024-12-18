using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync(ItemQueryObject query);  
        Task<Item> CreateAsync(Item ItemModel);
        Task<Item?> UpdateAsync(int id, Item ItemModel);
        Task<Item?> GetByIdAsync(int id);
        Task<Item?> DeleteAsync(int id);    
    }
}