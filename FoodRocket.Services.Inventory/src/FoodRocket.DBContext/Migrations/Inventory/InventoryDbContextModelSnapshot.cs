﻿// <auto-generated />
using System;
using FoodRocket.DBContext.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodRocket.DBContext.Migrations.Inventory
{
    [DbContext(typeof(InventoryDbContext))]
    partial class InventoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("inventory")
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long?>("MainUnitOfMeasureUnitOfMeasureId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ProductId");

                    b.HasIndex("MainUnitOfMeasureUnitOfMeasureId");

                    b.ToTable("Products", "inventory");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.ProductUnitOfMeasure", b =>
                {
                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("UnitOfMeasureId")
                        .HasColumnType("bigint");

                    b.HasKey("ProductId", "UnitOfMeasureId");

                    b.HasIndex("UnitOfMeasureId");

                    b.ToTable("ProductUnitOfMeasures", "inventory");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.Storage", b =>
                {
                    b.Property<long>("StorageId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("StorageId");

                    b.ToTable("Storages", "inventory");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.StorageProduct", b =>
                {
                    b.Property<long>("StorageId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(5,2)");

                    b.Property<long?>("UnitOfMeasureId")
                        .HasColumnType("bigint");

                    b.HasKey("StorageId", "ProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UnitOfMeasureId");

                    b.ToTable("ProductsInStorages", "inventory");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.UnitOfMeasure", b =>
                {
                    b.Property<long>("UnitOfMeasureId")
                        .HasColumnType("bigint");

                    b.Property<long?>("BaseOfUnitOfMId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsBase")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFractional")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Ratio")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("UnitOfMeasureId");

                    b.ToTable("UnitOfMeasures", "inventory");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.Product", b =>
                {
                    b.HasOne("FoodRocket.DBContext.Models.Inventory.UnitOfMeasure", "MainUnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("MainUnitOfMeasureUnitOfMeasureId");

                    b.Navigation("MainUnitOfMeasure");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.ProductUnitOfMeasure", b =>
                {
                    b.HasOne("FoodRocket.DBContext.Models.Inventory.Product", "Product")
                        .WithMany("UnitOfMeasuresLink")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRocket.DBContext.Models.Inventory.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany("ProductsLink")
                        .HasForeignKey("UnitOfMeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("UnitOfMeasure");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.StorageProduct", b =>
                {
                    b.HasOne("FoodRocket.DBContext.Models.Inventory.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRocket.DBContext.Models.Inventory.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodRocket.DBContext.Models.Inventory.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureId");

                    b.Navigation("Product");

                    b.Navigation("Storage");

                    b.Navigation("UnitOfMeasure");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.Product", b =>
                {
                    b.Navigation("UnitOfMeasuresLink");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Inventory.UnitOfMeasure", b =>
                {
                    b.Navigation("ProductsLink");
                });
#pragma warning restore 612, 618
        }
    }
}
