using PizzaPalatsetServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPalatsetServer.JsonObjects
{
    public class JsonPizza
    {
        public Pizza Pizza { get; set; }
        public List<int> Ingredients { get; set; }
    }
}
