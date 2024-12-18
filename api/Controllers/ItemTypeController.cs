using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.ItemType;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/itemtype")]
    [ApiController]
    public class ItemTypeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IItemTypeRepository _itemTypeRepo;
        public ItemTypeController(ApplicationDBContext context, IItemTypeRepository itemTypeRepo)
        {
            _itemTypeRepo = itemTypeRepo;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemTypes = await _itemTypeRepo.GetAllAsync();

            var itemDto = itemTypes.Select(s => s.ToItemTypeDto()).ToList();

            return Ok(itemDto);
        }  

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ItemType = await _itemTypeRepo.GetByIdAsync(id);

            if (ItemType == null)
            {
                return NotFound();
            }

            return Ok(ItemType.ToItemTypeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemTypeDto ItemTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ItemTypeModel = ItemTypeDto.ToItemTypeFromCreate();

            await _itemTypeRepo.CreateAsync(ItemTypeModel);
            return CreatedAtAction(nameof(GetById), new { id = ItemTypeModel.Id }, ItemTypeModel.ToItemTypeDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateItemTypeRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ItemType = await _itemTypeRepo.UpdateAsync(id, updateDto.ToItemTypeFromUpdate());

            if (ItemType == null)
            {
                return NotFound("Item Type not found");
            }

            return Ok(ItemType.ToItemTypeDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ItemTypeModel = await _itemTypeRepo.DeleteAsync(id);

            if (ItemTypeModel == null)
            {
                return NotFound("Item Type does not exist");
            }

            return Ok(ItemTypeModel);
        }     
    }
}