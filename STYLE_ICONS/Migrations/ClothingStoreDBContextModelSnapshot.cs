﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(ClothingStoreDBContext))]
    partial class ClothingStoreDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebApplication1.Models.Basket", b =>
                {
                    b.Property<int>("Idbasket")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDBasket")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Fkorder")
                        .HasColumnName("FKOrder");

                    b.Property<int>("Fkproduct")
                        .HasColumnName("FKProduct");

                    b.Property<int>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("Idbasket");

                    b.HasIndex("Fkorder");

                    b.HasIndex("Fkproduct");

                    b.ToTable("Basket");
                });

            modelBuilder.Entity("WebApplication1.Models.Catalog", b =>
                {
                    b.Property<int>("Idcategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDCategory")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CatalogName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Idcategory");

                    b.HasIndex("CatalogName")
                        .IsUnique()
                        .HasName("IX_Catalog");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("WebApplication1.Models.Order", b =>
                {
                    b.Property<int>("Idorder")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDOrder")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(300);

                    b.Property<int>("Cost");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime");

                    b.Property<int>("Fkstatus")
                        .HasColumnName("FKStatus");

                    b.Property<string>("Fkuser")
                        .HasColumnName("FKUser");

                    b.Property<DateTime?>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Idorder");

                    b.HasIndex("Fkstatus");

                    b.HasIndex("Fkuser");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebApplication1.Models.OrderStatus", b =>
                {
                    b.Property<int>("Idstatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDStatus")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Idstatus");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("WebApplication1.Models.Product", b =>
                {
                    b.Property<int>("Idproduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDProduct")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Fkcategory")
                        .HasColumnName("FKCategory");

                    b.Property<int>("Price");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("QuantityInStock");

                    b.HasKey("Idproduct");

                    b.HasIndex("Fkcategory");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("WebApplication1.Models.StoreUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(20);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApplication1.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApplication1.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApplication1.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebApplication1.Models.StoreUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApplication1.Models.Basket", b =>
                {
                    b.HasOne("WebApplication1.Models.Order", "FkorderNavigation")
                        .WithMany("Basket")
                        .HasForeignKey("Fkorder")
                        .HasConstraintName("FK_Basket_Order");

                    b.HasOne("WebApplication1.Models.Product", "FkproductNavigation")
                        .WithMany("Basket")
                        .HasForeignKey("Fkproduct")
                        .HasConstraintName("FK_Basket_Product");
                });

            modelBuilder.Entity("WebApplication1.Models.Order", b =>
                {
                    b.HasOne("WebApplication1.Models.OrderStatus", "FkstatusNavigation")
                        .WithMany("Order")
                        .HasForeignKey("Fkstatus")
                        .HasConstraintName("FK_Order_OrderStatus");

                    b.HasOne("WebApplication1.Models.StoreUser", "FkuserNavigation")
                        .WithMany("Order")
                        .HasForeignKey("Fkuser")
                        .HasConstraintName("FK_Order_StoreUser");
                });

            modelBuilder.Entity("WebApplication1.Models.Product", b =>
                {
                    b.HasOne("WebApplication1.Models.Catalog", "FkcategoryNavigation")
                        .WithMany("Product")
                        .HasForeignKey("Fkcategory")
                        .HasConstraintName("FK_Product_Catalog");
                });
#pragma warning restore 612, 618
        }
    }
}
