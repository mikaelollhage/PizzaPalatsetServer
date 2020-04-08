using PizzaPalatsetServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPalatsetServer.JsonObjects
{
    public class JsonOrder
    {
        public int OrderId { get; set; }
        public List<Pizza> PizzaOrderContents { get; set; }
        public List<Drink> DrinkOrderContents { get; set; }
        public decimal TotalOrderCost { get; set; }
    }
}
