using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPalatsetServer.Models
{
    public class OrderPizza
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }

    }
}
