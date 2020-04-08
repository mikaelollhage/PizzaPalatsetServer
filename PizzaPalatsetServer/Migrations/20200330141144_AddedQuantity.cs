using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaPalatsetServer.Migrations
{
    public partial class AddedQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderPizza",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDrink",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderPizza");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDrink");
        }
    }
}
