using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ItemCategory;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public ItemCategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ItemCategory>> GetAllAsync(ItemCategoryQueryObject query)
        {
            var itemCategories = _context.ItemCategory.Include(c => c.Item).ThenInclude(x => x.Type).Include(a => a.Category).ThenInclude(y => y.Parent).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CategoryDescription))
            {
                itemCategories = itemCategories.Where(s => s.Category.Description.Contains(query.CategoryDescription));
            };

            if (!string.IsNullOrWhiteSpace(query.BookName))
            {
                itemCategories = itemCategories.Where(s => s.Item.Name.Contains(query.BookName));
            };

            if (!string.IsNullOrWhiteSpace(query.TypeName))
            {
                itemCategories = itemCategories.Where(s => s.Item.Type.Name.Contains(query.TypeName));
            };           

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    itemCategories = query.IsDecsending ? itemCategories.OrderByDescending(s => s.Item.Name) : itemCategories.OrderBy(s => s.Item.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await itemCategories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<ItemCategory?> GetByIdAsync(int itemId, int categoryId)
        {
            return await _context.ItemCategory.FindAsync(itemId, categoryId);//.Where(x => x.ItemId == itemId && x.CategoryId == categoryId).Include(c => c.Item).Include(x => x.Category).FirstAsync();
        }

        public async Task<ItemCategory> CreateAsync(ItemCategory itemCategoryModel)
        {
            await _context.ItemCategory.AddAsync(itemCategoryModel);
            await _context.SaveChangesAsync();
            return itemCategoryModel;
        }

        public async Task<ItemCategory?> DeleteAsync(int itemId, int categoryId)
        {
            var itemCategoryModel = await _context.ItemCategory.FindAsync(itemId, categoryId);

            if (itemCategoryModel == null)
            {
                return null;
            }

            _context.ItemCategory.Remove(itemCategoryModel);
            await _context.SaveChangesAsync();
            return itemCategoryModel;
        }
    }
}