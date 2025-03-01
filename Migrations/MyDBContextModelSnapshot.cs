﻿// <auto-generated />
using System;
using Kick_X.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kick_X.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Customer", b =>
                {
                    b.Property<int>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("customer_id"));

                    b.Property<string>("customer_address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("customer_pincode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("customer_id");

                    b.ToTable("tbl_customer");
                });

            modelBuilder.Entity("Kick_X.Models.Admin", b =>
                {
                    b.Property<int>("admin_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("admin_id"));

                    b.Property<string>("admin_email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("admin_name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("admin_password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("admin_id");

                    b.ToTable("tbl_admin");
                });

            modelBuilder.Entity("Kick_X.Models.CartItem", b =>
                {
                    b.Property<int>("cartitem_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cartitem_id"));

                    b.Property<int>("cartitem_product_quantity")
                        .HasColumnType("int");

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.HasKey("cartitem_id");

                    b.HasIndex("customer_id");

                    b.HasIndex("product_id");

                    b.ToTable("tbl_cartitem");
                });

            modelBuilder.Entity("Kick_X.Models.Category", b =>
                {
                    b.Property<int>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("category_id"));

                    b.Property<string>("category_name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("category_id");

                    b.ToTable("tbl_category");
                });

            modelBuilder.Entity("Kick_X.Models.Order", b =>
                {
                    b.Property<int>("order_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("order_id"));

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("order_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("order_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("order_subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("order_uid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("order_id");

                    b.HasIndex("customer_id");

                    b.ToTable("tbl_order");
                });

            modelBuilder.Entity("Kick_X.Models.OrderItem", b =>
                {
                    b.Property<int>("orderitem_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderitem_id"));

                    b.Property<int>("customer_id")
                        .HasColumnType("int");

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<int>("orderitem_product_quantity")
                        .HasColumnType("int");

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.HasKey("orderitem_id");

                    b.HasIndex("customer_id");

                    b.HasIndex("order_id");

                    b.HasIndex("product_id");

                    b.ToTable("tbl_orderitem");
                });

            modelBuilder.Entity("Kick_X.Models.Product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("product_id"));

                    b.Property<int>("category_id")
                        .HasColumnType("int");

                    b.Property<string>("product_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("product_price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("product_id");

                    b.HasIndex("category_id");

                    b.ToTable("tbl_product");
                });

            modelBuilder.Entity("Kick_X.Models.CartItem", b =>
                {
                    b.HasOne("Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("customer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kick_X.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kick_X.Models.Order", b =>
                {
                    b.HasOne("Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("customer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Kick_X.Models.OrderItem", b =>
                {
                    b.HasOne("Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("customer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kick_X.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kick_X.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kick_X.Models.Product", b =>
                {
                    b.HasOne("Kick_X.Models.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Kick_X.Models.Category", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kick_X.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
