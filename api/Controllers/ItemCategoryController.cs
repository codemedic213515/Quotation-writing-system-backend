using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ItemCategory;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/itemcategory")]
    public class ItemCategoryController : ControllerBase
    {
        readonly ApplicationDBContext _context;
        private readonly IItemCategoryRepository _itemCategoryRepo;
        public ItemCategoryController(ApplicationDBContext context, IItemCategoryRepository itemCategoryRepo)
        {
            _itemCategoryRepo = itemCategoryRepo;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ItemCategoryQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemCategories = await _itemCategoryRepo.GetAllAsync(query);

            var itemCategoryDto = itemCategories.Select(s => s.ToItemCategoryDto()).ToList();

            return Ok(itemCategoryDto);
        }   

        [HttpGet("{itemId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int itemId, [FromQuery] int categoryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemCategory = await _itemCategoryRepo.GetByIdAsync(itemId, categoryId);

            if (itemCategory == null)
            {
                return NotFound();
            }

            return Ok(itemCategory.ToItemCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemCategoryDto itemCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemCategoryModel = itemCategoryDto.ToItemCategoryFromCreate();

            await _itemCategoryRepo.CreateAsync(itemCategoryModel);
            return CreatedAtAction(nameof(GetById), new { itemId = itemCategoryModel.ItemId, categoryId = itemCategoryModel.CategoryId }, itemCategoryModel.ToItemCategoryDto());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int bookId, [FromQuery] int authorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookAuthorModel = await _itemCategoryRepo.DeleteAsync(bookId, authorId);

            if (bookAuthorModel == null)
            {
                return NotFound("Item Category does not exist");
            }

            return Ok(bookAuthorModel);
        }   
    }
}