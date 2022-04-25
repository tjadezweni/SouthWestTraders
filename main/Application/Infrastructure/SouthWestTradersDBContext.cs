using Microsoft.EntityFrameworkCore;
using Application.Infrastructure.Entities;

namespace Application.Infrastructure
{
    public partial class SouthWestTradersDBContext : DbContext
    {
        public SouthWestTradersDBContext()
        {
        }

        public SouthWestTradersDBContext(DbContextOptions<SouthWestTradersDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderState> OrderStates { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=SouthWestTradersDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasIndex(e => e.Name, "UQ__Order__737584F6652007FA")
                    .IsUnique();

                entity.Property(e => e.CreatedDateUtc)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDateUTC");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.OrderState)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStateId)
                    .HasConstraintName("FK__Order__OrderStat__693CA210");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Order__ProductId__6754599E");
            });

            modelBuilder.Entity<OrderState>(entity =>
            {
                entity.ToTable("OrderState");

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.Name, "UQ__Product__737584F691F244F6")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Stock__ProductId__628FA481");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
