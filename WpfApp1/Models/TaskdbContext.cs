using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WpfApp1
{
    public partial class TaskdbContext : DbContext
    {
        public TaskdbContext()
        {
            Database.EnsureCreated();
        }

        public TaskdbContext(DbContextOptions<TaskdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<DishInOrder> DishInOrders { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Taskdb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dish");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<DishInOrder>(entity =>
            {
                entity.ToTable("DishInOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DishId).HasColumnName("DishID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.DishInOrders)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DishInOrder_Dish");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateCreate).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UresId).HasColumnName("UresID");

                entity.HasOne(d => d.Ures)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_DishInOrder");

                entity.HasOne(d => d.UresNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UresId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
