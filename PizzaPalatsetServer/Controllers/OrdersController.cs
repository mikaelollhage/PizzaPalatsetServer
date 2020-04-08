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
    public class OrdersController : ControllerBase
    {
        private readonly PizzaPalatsetServerContext _context;

        public OrdersController(PizzaPalatsetServerContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id,JsonOrder jsonOrder)
        {
            //Create a server order object using the inparamaters
            Order order = new Order
            {
                OrderId = id,
                TotalOrderCost = jsonOrder.TotalOrderCost
            };
            List<Pizza> pizzas = jsonOrder.PizzaOrderContents;
            List<Drink> drinks = jsonOrder.DrinkOrderContents;
                       
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            //Remove everything from OrderPizza composite table having the same orderId as supplied to the method
            _context.OrderPizza.RemoveRange(_context.OrderPizza.Select(op => op).Where(op => op.OrderId == id));
            //Remove everything from OrderDrink composite table having the same orderId as supplied to the method
            _context.OrderDrink.RemoveRange(_context.OrderDrink.Select(op => op).Where(op => op.OrderId == id));

            //Recreate the content in the composite tables using the new info. Add one entry per row in the lists
            pizzas.ForEach(p => _context.OrderPizza.Add(new OrderPizza() { OrderId = id, PizzaId = p.PizzaId, Quantity = p.Quantity }));
            drinks.ForEach(d => _context.OrderDrink.Add(new OrderDrink() { OrderId = id, DrinkId = d.DrinkId, Quantity = d.Quantity }));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<JsonOrder>> PostOrder(JsonOrder jsonOrder)
        {
            Order order = new Order();
            //Create server-Order from CLient-Order
            order.TotalOrderCost = jsonOrder.TotalOrderCost;

            //Create lists of Food from the client order. Used to populate the composit tables concerned
            List<Pizza> pizzas = jsonOrder.PizzaOrderContents;
            List<Drink> drinks = jsonOrder.DrinkOrderContents;

            //Write new order to database
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            //Get the new orderID
            int orderId = order.OrderId;
            //give the client object the new orderID since it is needed in the client app after the return
            jsonOrder.OrderId = orderId;

            //Loop through the pizza list and write a new OrderPizza to the database using its contents
            pizzas.ForEach(p => _context.OrderPizza.Add(new OrderPizza() { OrderId = orderId, PizzaId=p.PizzaId, Quantity=p.Quantity }));
            //Loop through the pizza list and write a new OrderPizza to the database using its contents
            drinks.ForEach(d => _context.OrderDrink.Add(new OrderDrink() { OrderId = orderId, DrinkId=d.DrinkId, Quantity=d.Quantity }));
            await _context.SaveChangesAsync();

            //Return a JsonOrder instead of Order to avoid "Possible object cycle..." and to be able to deserialize towards the already existing Order-class
            return CreatedAtAction("GetOrder", new { id = order.OrderId }, jsonOrder);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
