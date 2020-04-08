using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPalatsetServer.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal TotalOrderCost { get; set; }
        public IList<OrderDrink> OrderDrink { get; set; }
        public IList<OrderPizza> OrderPizza { get; set; }
    }
}
