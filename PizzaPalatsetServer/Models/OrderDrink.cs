using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPalatsetServer.Models
{
    public class OrderDrink
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
    }
}
