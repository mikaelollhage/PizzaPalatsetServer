using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaPalatsetServer.Data;
using PizzaPalatsetServer.Models;

namespace PizzaPalatsetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly PizzaPalatsetServerContext _context;

        public DrinksController(PizzaPalatsetServerContext context)
        {
            _context = context;
        }

        // GET: api/Drinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drink>>> GetDrink()
        {
            var result = await _context.Drink
                .Select(d => new { d.DrinkId, d.Name, d.Price, d.Image })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drink>> GetDrink(int id)
        {
            var drink = await _context.Drink.FindAsync(id);

            if (drink == null)
            {
                return NotFound();
            }

            return drink;
        }

        // POST: api/Drinks
        [HttpPost]
        public async Task<ActionResult<Drink>> PostDrink(Drink drink)
        {
            _context.Drink.Add(drink);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrink", new { id = drink.DrinkId }, drink);
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drink>> DeleteDrink(int id)
        {
            var drink = await _context.Drink.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            _context.Drink.Remove(drink);
            await _context.SaveChangesAsync();

            return drink;
        }
        private bool DrinkExists(int id)
        {
            return _context.Drink.Any(e => e.DrinkId == id);
        }
    }
}
