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
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDBContext _context;
        public ItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Item>> GetAllAsync(ItemQueryObject query)
        {
            var items = _context.Items.Include(c => c.Type).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.BookName))
            {
                items = items.Where(s => s.Name.Contains(query.BookName));
            };

            if (!string.IsNullOrWhiteSpace(query.TypeName))
            {
                items = items.Where(s => s.Type.Name.Contains(query.TypeName));
            };           

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    items = query.IsDecsending ? items.OrderByDescending(s => s.Name) : items.OrderBy(s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await items.Skip(skipNumber).Take(query.PageSize).ToListAsync();        
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Item> CreateAsync(Item itemModel)
        {
            await _context.Items.AddAsync(itemModel);
            await _context.SaveChangesAsync();
            return itemModel;
        }

        public async Task<Item?> UpdateAsync(int id, Item itemModel)
        {
            var existingItem = await _context.Items.FindAsync(id);

            if (existingItem == null)
            {
                return null;
            }

            existingItem.Name = itemModel.Name;
            existingItem.TypeId = itemModel.TypeId;
            existingItem.ImageFileSpec = itemModel.ImageFileSpec;
            existingItem.Description = itemModel.Description;
            existingItem.UnitCost = itemModel.UnitCost;
            existingItem.UnitPrice = itemModel.UnitPrice;

            await _context.SaveChangesAsync();

            return existingItem;
        }

        public async Task<Item?> DeleteAsync(int id)
        {
            var itemModel = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (itemModel == null)
            {
                return null;
            }

            _context.Items.Remove(itemModel);
            await _context.SaveChangesAsync();
            return itemModel;
        }
    }
}