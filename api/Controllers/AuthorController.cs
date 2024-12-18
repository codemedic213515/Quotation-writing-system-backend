using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Author;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IAuthorRepository _authorRepo;
        public AuthorController(ApplicationDBContext context, IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] AuthorQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authors = await _authorRepo.GetAllAsync(query);

            var authorDto = authors.Select(s => s.ToAuthorDto()).ToList();

            return Ok(authorDto);
        }     

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = await _authorRepo.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author.ToAuthorDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto authorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorModel = authorDto.ToAuthorFromCreate();

            await _authorRepo.CreateAsync(authorModel);
            return CreatedAtAction(nameof(GetById), new { id = authorModel.Id }, authorModel.ToAuthorDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuthorRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = await _authorRepo.UpdateAsync(id, updateDto.ToAuthorFromUpdate());

            if (author == null)
            {
                return NotFound("Author not found");
            }

            return Ok(author.ToAuthorDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorModel = await _authorRepo.DeleteAsync(id);

            if (authorModel == null)
            {
                return NotFound("Author does not exist");
            }

            return Ok(authorModel);
        }   
    }
}