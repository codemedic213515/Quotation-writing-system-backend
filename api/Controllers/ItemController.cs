using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Item;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IItemRepository _itemRepo;
        public ItemController(ApplicationDBContext context, IItemRepository itemRepo)
        {
            _itemRepo = itemRepo;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] ItemQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var items = await _itemRepo.GetAllAsync(query);

            var itemDto = items.Select(s => s.ToItemDto()).ToList();

            return Ok(itemDto);
        }    

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await _itemRepo.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item.ToItemDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = itemDto.ToItemFromCreate();

            await _itemRepo.CreateAsync(itemModel);
            return CreatedAtAction(nameof(GetById), new { id = itemModel.Id }, itemModel.ToItemDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateItemRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = await _itemRepo.UpdateAsync(id, updateDto.ToItemFromUpdate());

            if (item == null)
            {
                return NotFound("Item not found");
            }

            return Ok(item.ToItemDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemModel = await _itemRepo.DeleteAsync(id);

            if (itemModel == null)
            {
                return NotFound("Item does not exist");
            }

            return Ok(itemModel);
        }    
    }
}