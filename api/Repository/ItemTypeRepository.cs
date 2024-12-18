using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly ApplicationDBContext _context;
        public ItemTypeRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<ItemType>> GetAllAsync()
        {
            var itemTypes = _context.ItemTypes.AsQueryable();

            return await itemTypes.ToListAsync();
        }

        public async Task<ItemType?> GetByIdAsync(int id)
        {
            return await _context.ItemTypes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ItemType> CreateAsync(ItemType ItemTypeModel)
        {
            await _context.ItemTypes.AddAsync(ItemTypeModel);
            await _context.SaveChangesAsync();
            return ItemTypeModel;
        }

        public async Task<ItemType?> UpdateAsync(int id, ItemType ItemTypeModel)
        {
            var existingItemType = await _context.ItemTypes.FindAsync(id);

            if (existingItemType == null)
            {
                return null;
            }

            existingItemType.Name = ItemTypeModel.Name;
            existingItemType.Description = ItemTypeModel.Description;

            await _context.SaveChangesAsync();

            return existingItemType;
        }

        public async Task<ItemType?> DeleteAsync(int id)
        {
            var ItemTypeModel = await _context.ItemTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (ItemTypeModel == null)
            {
                return null;
            }

            _context.ItemTypes.Remove(ItemTypeModel);
            await _context.SaveChangesAsync();
            return ItemTypeModel;
        }
    }
}