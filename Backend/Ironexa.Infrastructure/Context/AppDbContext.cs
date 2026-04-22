using Ironexa.Domain.Entities;
using Ironexa.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ironexa.Infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderItem>()
                .HasOne(o => o.Measurement)
                .WithOne(m => m.OrderItem)
                .HasForeignKey<Measurement>(m => m.OrderItemId);

            builder.Entity<OrderItem>()
                .Property(x => x.RatePerKg)
                .HasColumnType("decimal(18, 2)");
            builder.Entity<OrderItem>()
                .Property(x => x.FinalWeight)
                .HasColumnType("decimal(18, 2)");
            builder.Entity<OrderItem>()
                .Property(x => x.TotalAmount)
                .HasColumnType("decimal(18, 2)");
            builder.Entity<Payment>()
                .Property(x => x.Amount)
                .HasColumnType("decimal(18, 2)");

        }
    }
}
