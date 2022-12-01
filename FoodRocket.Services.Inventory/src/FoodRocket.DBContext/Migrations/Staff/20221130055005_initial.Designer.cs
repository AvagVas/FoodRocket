﻿// <auto-generated />
using System;
using FoodRocket.DBContext.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodRocket.DBContext.Migrations.Staff
{
    [DbContext(typeof(StaffDbContext))]
    [Migration("20221130055005_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("staff")
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FoodRocket.DBContext.Models.Staff.Employee", b =>
                {
                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SocialSecurityNumber")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees", "staff");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Staff.Manager", b =>
                {
                    b.Property<long>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("FinishedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ManagerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Managers", "staff");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Staff.Waiter", b =>
                {
                    b.Property<long>("WaiterId")
                        .HasColumnType("bigint");

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("FinishedOn")
                        .HasColumnType("datetime2");

                    b.Property<long?>("ManagerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("WaiterId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Waiters", "staff");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Staff.Manager", b =>
                {
                    b.HasOne("FoodRocket.DBContext.Models.Staff.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Staff.Waiter", b =>
                {
                    b.HasOne("FoodRocket.DBContext.Models.Staff.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("FoodRocket.DBContext.Models.Staff.Manager", "Manager")
                        .WithMany("Waiters")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Employee");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("FoodRocket.DBContext.Models.Staff.Manager", b =>
                {
                    b.Navigation("Waiters");
                });
#pragma warning restore 612, 618
        }
    }
}
