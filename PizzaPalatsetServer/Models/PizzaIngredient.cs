using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPalatsetServer.Models
{
    public class PizzaIngredient
    {
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
