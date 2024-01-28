﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecurityManager_Fun.Data;

#nullable disable

namespace SecurityManager_Fun.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SecurityManager_Fun.Model.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Deduction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("CountryID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPercentage")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(10,4)");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Deductions");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CountryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CountryID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GrossRate")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("RoleID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Payment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfLatestModification")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DeductionsValue")
                        .HasColumnType("decimal(10,4)");

                    b.Property<decimal>("DefaultValue")
                        .HasColumnType("decimal(10,4)");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<decimal>("FinalValue")
                        .HasColumnType("decimal(10,4)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.PaymentDeduction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("DeductionID")
                        .HasColumnType("int");

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DeductionID");

                    b.HasIndex("PaymentID");

                    b.ToTable("PaymentDeductions");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.WorkSchedule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<int>("DayType")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<int>("WorkHours")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("WorkSchedules");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Deduction", b =>
                {
                    b.HasOne("SecurityManager_Fun.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Department", b =>
                {
                    b.HasOne("SecurityManager_Fun.Model.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Employee", b =>
                {
                    b.HasOne("SecurityManager_Fun.Model.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentID");

                    b.HasOne("SecurityManager_Fun.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID");

                    b.Navigation("Department");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Payment", b =>
                {
                    b.HasOne("SecurityManager_Fun.Model.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.PaymentDeduction", b =>
                {
                    b.HasOne("SecurityManager_Fun.Model.Deduction", "Deduction")
                        .WithMany()
                        .HasForeignKey("DeductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecurityManager_Fun.Model.Payment", "Payment")
                        .WithMany("PaymentDeductions")
                        .HasForeignKey("PaymentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deduction");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.WorkSchedule", b =>
                {
                    b.HasOne("SecurityManager_Fun.Model.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SecurityManager_Fun.Model.Payment", b =>
                {
                    b.Navigation("PaymentDeductions");
                });
#pragma warning restore 612, 618
        }
    }
}
