using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaPalatsetServer.Data;
using PizzaPalatsetServer.JsonObjects;
using PizzaPalatsetServer.Models;

namespace PizzaPalatsetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzaPalatsetServerContext _context;

        public PizzasController(PizzaPalatsetServerContext context)
        {
            _context = context;
        }

        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            //Creates an object which can be directly received by the client
            var result = await _context.Pizza.Include(p => p.PizzaIngredient)
                .ThenInclude(pi => pi.Ingredient)
                .Select(p => new { p.PizzaId, p.Name, p.Price, p.Image, ingredients = p.PizzaIngredient.Select(pi => new {pi.Ingredient.Name }) })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // POST: api/Pizzas
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza(JsonPizza jsonPizza)
        {
            //Save ingredients to list
            List<int> ingredients = jsonPizza.Ingredients;
            //Save the JsonPizza.Pizza property to a Pizza object
            Pizza pizza = jsonPizza.Pizza;

            //Save pizza to Pizza table
            _context.Pizza.Add(pizza);
            await _context.SaveChangesAsync();

            //get the new pizzaId
            int pizzaId = pizza.PizzaId;

            //Loop through all ingredients and save them to the PizzaIngredient table
            ingredients.ForEach(i => _context.PizzaIngredient.Add(new PizzaIngredient() { PizzaId = pizzaId, IngredientId = i }));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPizza", new { id = pizza.PizzaId }, pizza);
        }

        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pizza>> DeletePizza(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();

            return pizza;
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizza.Any(e => e.PizzaId == id);
        }
    }
}
