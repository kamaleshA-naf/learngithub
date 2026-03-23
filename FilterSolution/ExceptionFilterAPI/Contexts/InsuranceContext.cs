using ExceptionFilterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExceptionFilterAPI.Contexts
{
    public class InsuranceContext : DbContext
    {
        public InsuranceContext(DbContextOptions<InsuranceContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Policy> Policies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Policies)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);
            modelBuilder.Entity<Insurance>()
                .HasMany(i => i.Policies)
                .WithOne(p => p.Insurance)
                .HasForeignKey(p => p.InsuranceNumber);

            modelBuilder.Entity<Insurance>().HasKey(i => i.InsuranceNumber).HasName("PK_InsuranceNumber");
            modelBuilder.Entity<Policy>().HasKey(p => p.PolicyNumber).HasName("PK_PolicyNumber");
            modelBuilder.Entity<Customer>().HasKey(c => c.Id).HasName("PK_CustomerId");

            modelBuilder.Entity<Insurance>().HasData(
                new Insurance { InsuranceNumber = 101, Name = "Health Insurance", PayingDuration = 12, ReturnDuration = 24, PayoutPerMonth = 1000, Balance = 5000 },
                new Insurance { InsuranceNumber = 102, Name = "Car Insurance", PayingDuration = 6, ReturnDuration = 12, PayoutPerMonth = 500, Balance = 2000 }
            );
            modelBuilder.Entity<Customer>().HasData(
               new Customer { Id = 1, Name = "John Doe", DateOfBirth = new DateTime(1990, 1, 1), Email = "john@gmail.com", PhoneNumber = "9876543210" }
               );
        }
    }
}
