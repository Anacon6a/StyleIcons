using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public partial class ClothingStoreDBContext : IdentityDbContext<StoreUser>
    {
        #region ​Constructor
        public ClothingStoreDBContext(DbContextOptions<ClothingStoreDBContext> options)
            : base(options)
        {
        }
		#endregion
		public virtual DbSet<Basket> Basket { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<StoreUser> StoreUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=USER-PC;Database=OnlineClothingStoreDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => e.Idbasket);

                entity.Property(e => e.Idbasket).HasColumnName("IDBasket");

                entity.Property(e => e.Fkorder).HasColumnName("FKOrder");

                entity.Property(e => e.Fkproduct).HasColumnName("FKProduct");

                entity.HasOne(d => d.FkorderNavigation)
                    .WithMany(p => p.Basket)
                    .HasForeignKey(d => d.Fkorder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Basket_Order");

                entity.HasOne(d => d.FkproductNavigation)
                    .WithMany(p => p.Basket)
                    .HasForeignKey(d => d.Fkproduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Basket_Product");
            });

            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.HasKey(e => e.Idcategory);

                entity.HasIndex(e => e.CatalogName)
                    .HasName("IX_Catalog")
                    .IsUnique();

                entity.Property(e => e.Idcategory).HasColumnName("IDCategory");

                entity.Property(e => e.CatalogName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Idorder);

                entity.Property(e => e.Idorder).HasColumnName("IDOrder");

                entity.Property(e => e.Address).HasMaxLength(300);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.Fkstatus).HasColumnName("FKStatus");

                entity.Property(e => e.Fkuser).HasColumnName("FKUser");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FkstatusNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Fkstatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderStatus");

                entity.HasOne(d => d.FkuserNavigation)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.Fkuser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_StoreUser");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.Idstatus);

                entity.Property(e => e.Idstatus).HasColumnName("IDStatus");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Idproduct);

                entity.Property(e => e.Idproduct).HasColumnName("IDProduct");

                entity.Property(e => e.Fkcategory).HasColumnName("FKCategory");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductImage).HasColumnName("ProductImage")
                    .IsRequired();

                entity.HasOne(d => d.FkcategoryNavigation)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.Fkcategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Catalog");
            });

            modelBuilder.Entity<StoreUser>(entity =>
            {

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MiddleName).HasMaxLength(20);

            });

        }
    }
}
