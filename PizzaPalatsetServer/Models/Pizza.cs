﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPalatsetServer.Models
{
    public class Pizza
    {        
        public int PizzaId { get; set; }        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public IList<OrderPizza> OrderPizza { get; set; }
        public IList<PizzaIngredient> PizzaIngredient { get; set; }
    }
}
