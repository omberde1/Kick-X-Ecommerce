using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kick_X.Migrations
{
    /// <inheritdoc />
    public partial class NewOrderUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "order_uid",
                table: "tbl_order",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "order_uid",
                table: "tbl_order");
        }
    }
}
