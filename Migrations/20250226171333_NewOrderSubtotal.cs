using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kick_X.Migrations
{
    /// <inheritdoc />
    public partial class NewOrderSubtotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "order_subtotal",
                table: "tbl_order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_subtotal",
                table: "tbl_order");
        }
    }
}
