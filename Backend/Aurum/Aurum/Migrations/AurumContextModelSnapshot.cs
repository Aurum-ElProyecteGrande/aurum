﻿// <auto-generated />
using System;
using Aurum.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aurum.Migrations
{
    [DbContext(typeof(AurumContext))]
    partial class AurumContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aurum.Data.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Aurum.Data.Entities.BasicLayout", b =>
                {
                    b.Property<int>("BasicLayoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BasicLayoutId"));

                    b.Property<string>("Chart1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BasicLayoutId");

                    b.ToTable("BasicLayouts");
                });

            modelBuilder.Entity("Aurum.Data.Entities.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurrencyId"));

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CurrencyId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Aurum.Data.Entities.DetailedLayout", b =>
                {
                    b.Property<int>("DetailedLayoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailedLayoutId"));

                    b.Property<string>("Chart1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart8")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart9")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetailedLayoutId");

                    b.ToTable("DetailedLayouts");
                });

            modelBuilder.Entity("Aurum.Data.Entities.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpenseCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ExpenseSubCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseId");

                    b.HasIndex("AccountId");

                    b.HasIndex("ExpenseCategoryId");

                    b.HasIndex("ExpenseSubCategoryId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Aurum.Data.Entities.ExpenseCategory", b =>
                {
                    b.Property<int>("ExpenseCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseCategoryId");

                    b.ToTable("ExpenseCategories");
                });

            modelBuilder.Entity("Aurum.Data.Entities.ExpenseSubCategory", b =>
                {
                    b.Property<int>("ExpenseSubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseSubCategoryId"));

                    b.Property<int>("ExpenseCategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBase")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpenseSubCategoryId");

                    b.HasIndex("ExpenseCategoryId");

                    b.ToTable("ExpenseSubCategories");
                });

            modelBuilder.Entity("Aurum.Data.Entities.Income", b =>
                {
                    b.Property<int>("IncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncomeId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IncomeCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncomeId");

                    b.HasIndex("AccountId");

                    b.HasIndex("IncomeCategoryId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("Aurum.Data.Entities.IncomeCategory", b =>
                {
                    b.Property<int>("IncomeCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncomeCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncomeCategoryId");

                    b.ToTable("IncomeCategories");
                });

            modelBuilder.Entity("Aurum.Data.Entities.RegularExpense", b =>
                {
                    b.Property<int>("RegularExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegularExpenseId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ExpenseCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("ExpenseSubcategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Regularity")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RegularExpenseId");

                    b.HasIndex("ExpenseCategoryId");

                    b.HasIndex("ExpenseSubcategoryId");

                    b.ToTable("RegularExpenses");
                });

            modelBuilder.Entity("Aurum.Data.Entities.RegularIncome", b =>
                {
                    b.Property<int>("RegularIncomeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RegularIncomeId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IncomeCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Regularity")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("RegularIncomeId");

                    b.HasIndex("IncomeCategoryId");

                    b.ToTable("RegularIncomes");
                });

            modelBuilder.Entity("Aurum.Data.Entities.ScienticLayout", b =>
                {
                    b.Property<int>("ScienticLayoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScienticLayoutId"));

                    b.Property<string>("Chart1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart5")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart6")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart7")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chart8")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScienticLayoutId");

                    b.ToTable("ScienticLayouts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Aurum.Data.Entities.Expense", b =>
                {
                    b.HasOne("Aurum.Data.Entities.Account", null)
                        .WithMany("Expenses")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurum.Data.Entities.ExpenseCategory", null)
                        .WithMany("Expenses")
                        .HasForeignKey("ExpenseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurum.Data.Entities.ExpenseSubCategory", null)
                        .WithMany("Expenses")
                        .HasForeignKey("ExpenseSubCategoryId");
                });

            modelBuilder.Entity("Aurum.Data.Entities.ExpenseSubCategory", b =>
                {
                    b.HasOne("Aurum.Data.Entities.ExpenseCategory", null)
                        .WithMany("ExpenseSubCategories")
                        .HasForeignKey("ExpenseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aurum.Data.Entities.Income", b =>
                {
                    b.HasOne("Aurum.Data.Entities.Account", null)
                        .WithMany("Incomes")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurum.Data.Entities.IncomeCategory", null)
                        .WithMany("Incomes")
                        .HasForeignKey("IncomeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aurum.Data.Entities.RegularExpense", b =>
                {
                    b.HasOne("Aurum.Data.Entities.ExpenseCategory", null)
                        .WithMany("RegularExpenses")
                        .HasForeignKey("ExpenseCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurum.Data.Entities.ExpenseSubCategory", null)
                        .WithMany("RegularExpenses")
                        .HasForeignKey("ExpenseSubcategoryId");
                });

            modelBuilder.Entity("Aurum.Data.Entities.RegularIncome", b =>
                {
                    b.HasOne("Aurum.Data.Entities.IncomeCategory", null)
                        .WithMany("RegularIncomes")
                        .HasForeignKey("IncomeCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Aurum.Data.Entities.Account", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Incomes");
                });

            modelBuilder.Entity("Aurum.Data.Entities.ExpenseCategory", b =>
                {
                    b.Navigation("ExpenseSubCategories");

                    b.Navigation("Expenses");

                    b.Navigation("RegularExpenses");
                });

            modelBuilder.Entity("Aurum.Data.Entities.ExpenseSubCategory", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("RegularExpenses");
                });

            modelBuilder.Entity("Aurum.Data.Entities.IncomeCategory", b =>
                {
                    b.Navigation("Incomes");

                    b.Navigation("RegularIncomes");
                });
#pragma warning restore 612, 618
        }
    }
}
