using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kick_X.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    admin_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    admin_email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    admin_password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_admin", x => x.admin_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_pincode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_customer", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    product_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_tbl_product_tbl_category_category_id",
                        column: x => x.category_id,
                        principalTable: "tbl_category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    order_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_tbl_order_tbl_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "tbl_customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_cartitem",
                columns: table => new
                {
                    cartitem_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cartitem_product_quantity = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_cartitem", x => x.cartitem_id);
                    table.ForeignKey(
                        name: "FK_tbl_cartitem_tbl_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "tbl_customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_cartitem_tbl_product_product_id",
                        column: x => x.product_id,
                        principalTable: "tbl_product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_orderitem",
                columns: table => new
                {
                    orderitem_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderitem_product_quantity = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_orderitem", x => x.orderitem_id);
                    table.ForeignKey(
                        name: "FK_tbl_orderitem_tbl_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "tbl_customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_orderitem_tbl_order_order_id",
                        column: x => x.order_id,
                        principalTable: "tbl_order",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_orderitem_tbl_product_product_id",
                        column: x => x.product_id,
                        principalTable: "tbl_product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cartitem_customer_id",
                table: "tbl_cartitem",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cartitem_product_id",
                table: "tbl_cartitem",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_customer_id",
                table: "tbl_order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orderitem_customer_id",
                table: "tbl_orderitem",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orderitem_order_id",
                table: "tbl_orderitem",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orderitem_product_id",
                table: "tbl_orderitem",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_category_id",
                table: "tbl_product",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_admin");

            migrationBuilder.DropTable(
                name: "tbl_cartitem");

            migrationBuilder.DropTable(
                name: "tbl_orderitem");

            migrationBuilder.DropTable(
                name: "tbl_order");

            migrationBuilder.DropTable(
                name: "tbl_product");

            migrationBuilder.DropTable(
                name: "tbl_customer");

            migrationBuilder.DropTable(
                name: "tbl_category");
        }
    }
}
