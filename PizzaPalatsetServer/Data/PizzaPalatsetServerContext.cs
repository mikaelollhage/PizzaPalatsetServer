using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaPalatsetServer.Models;

namespace PizzaPalatsetServer.Data
{
    public class PizzaPalatsetServerContext : DbContext
    {
        public PizzaPalatsetServerContext (DbContextOptions<PizzaPalatsetServerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDrink>().HasKey(sc => new { sc.DrinkId, sc.OrderId });
            modelBuilder.Entity<OrderDrink>()
                .HasOne<Order>(sc => sc.Order)
                .WithMany(s => s.OrderDrink)
                .HasForeignKey(sc => sc.OrderId);
            modelBuilder.Entity<OrderDrink>()
                .HasOne<Drink>(sc => sc.Drink)
                .WithMany(s => s.OrderDrink)
                .HasForeignKey(sc => sc.DrinkId);

            modelBuilder.Entity<OrderPizza>().HasKey(sc => new { sc.PizzaId, sc.OrderId });
            modelBuilder.Entity<OrderPizza>()
                .HasOne<Order>(sc => sc.Order)
                .WithMany(s => s.OrderPizza)
                .HasForeignKey(sc => sc.OrderId);
            modelBuilder.Entity<OrderPizza>()
                .HasOne<Pizza>(sc => sc.Pizza)
                .WithMany(s => s.OrderPizza)
                .HasForeignKey(sc => sc.PizzaId);

            modelBuilder.Entity<PizzaIngredient>().HasKey(sc => new { sc.PizzaId, sc.IngredientId });
            modelBuilder.Entity<PizzaIngredient>()
                .HasOne<Pizza>(sc => sc.Pizza)
                .WithMany(s => s.PizzaIngredient)
                .HasForeignKey(sc => sc.PizzaId);
            modelBuilder.Entity<PizzaIngredient>()
                .HasOne<Ingredient>(sc => sc.Ingredient)
                .WithMany(s => s.PizzaIngredient)
                .HasForeignKey(sc => sc.IngredientId);
        }
        public DbSet<PizzaPalatsetServer.Models.Drink> Drink { get; set; }

        public DbSet<PizzaPalatsetServer.Models.Pizza> Pizza { get; set; }

        public DbSet<PizzaPalatsetServer.Models.Order> Order { get; set; }

        public DbSet<PizzaPalatsetServer.Models.Ingredient> Ingredient { get; set; }

        public DbSet<PizzaPalatsetServer.Models.OrderDrink> OrderDrink { get; set; }

        public DbSet<PizzaPalatsetServer.Models.OrderPizza> OrderPizza { get; set; }

        public DbSet<PizzaPalatsetServer.Models.PizzaIngredient> PizzaIngredient { get; set; }
    }
}
