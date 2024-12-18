using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Publisher;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPublisherRepository _publisherRepo;
        public PublisherController(ApplicationDBContext context, IPublisherRepository publisherRepo)
        {
            _publisherRepo = publisherRepo;
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publishers = await _publisherRepo.GetAllAsync();

            var publisherDto = publishers.Select(s => s.ToPublisherDto()).ToList();

            return Ok(publisherDto);
        }     
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = await _publisherRepo.GetByIdAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher.ToPublisherDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePublisherDto publisherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherModel = publisherDto.ToPublisherFromCreate();

            await _publisherRepo.CreateAsync(publisherModel);
            return CreatedAtAction(nameof(GetById), new { id = publisherModel.Id }, publisherModel.ToPublisherDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePublisherRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisher = await _publisherRepo.UpdateAsync(id, updateDto.ToPublisherFromUpdate());

            if (publisher == null)
            {
                return NotFound("Publisher not found");
            }

            return Ok(publisher.ToPublisherDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherModel = await _publisherRepo.DeleteAsync(id);

            if (publisherModel == null)
            {
                return NotFound("Publisher does not exist");
            }

            return Ok(publisherModel);
        }   
    }
}