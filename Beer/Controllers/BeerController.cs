﻿using Microsoft.AspNetCore.Mvc;
using BeerVendor.Services;
using BeerVendor.Models;

namespace BeerVendor.Controllers
{
    public class BeerController : Controller
    {
        private readonly BeerService _service;

        public BeerController(BeerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Beer>>> GetAllBeersAsync()
        {
            var beers = await _service.GetAllBeersAsync();
            return Ok(beers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeerByIdAsync(int id)
        {
            var beer = await _service.GetBeerByIdAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return Ok(beer);
        }

        [HttpPost]
        public async Task<IActionResult> AddBeerAsync([FromBody] Beer beer)
        {
            await _service.AddBeerAsync(beer);
            return CreatedAtAction(nameof(GetBeerByIdAsync), new { id = beer.Id }, beer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeerAsync(int id, [FromBody] Beer beer)
        {
            try
            {
                await _service.UpdateBeerAsync(id, beer);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeerAsync(int id)
        {
            try
            {
                await _service.DeleteBeerAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
