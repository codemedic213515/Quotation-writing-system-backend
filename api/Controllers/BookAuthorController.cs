using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.BookAuthor;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/bookauthor")]
    public class BookAuthorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IBookAuthorRepository _bookAuthorRepo;
        public BookAuthorController(ApplicationDBContext context, IBookAuthorRepository bookAuthorRepo)
        {
            _bookAuthorRepo = bookAuthorRepo;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll([FromQuery] BookAuthorQueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookAuthors = await _bookAuthorRepo.GetAllAsync(query);

            var bookAuthorDto = bookAuthors.Select(s => s.ToBookAuthorDto()).ToList();

            return Ok(bookAuthorDto);
        } 

        [HttpGet("{bookId:int}")]
       public async Task<IActionResult> GetById([FromRoute] int bookId, [FromQuery] int authorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookAuthor = await _bookAuthorRepo.GetByIdAsync(bookId, authorId);

            if (bookAuthor == null)
            {
                return NotFound();
            }

            return Ok(bookAuthor.ToBookAuthorDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookAuthorDto bookAuthorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookAuthorModel = bookAuthorDto.ToBookAuthorFromCreate();

            await _bookAuthorRepo.CreateAsync(bookAuthorModel);
            return CreatedAtAction(nameof(GetById), new { bookId = bookAuthorModel.BookId, authorId = bookAuthorModel.AuthorId }, bookAuthorModel.ToBookAuthorDto());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int bookId, [FromQuery] int authorId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookAuthorModel = await _bookAuthorRepo.DeleteAsync(bookId, authorId);

            if (bookAuthorModel == null)
            {
                return NotFound("Book Author does not exist");
            }

            return Ok(bookAuthorModel);
        }            
    }
}