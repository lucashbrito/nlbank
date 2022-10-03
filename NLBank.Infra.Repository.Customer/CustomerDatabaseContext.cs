using Microsoft.EntityFrameworkCore;

namespace NLBank.Infra.Repository.Customer
{
    public class CustomerDatabaseContext : DbContext
    {
        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Domain.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TransactionMapping(modelBuilder);
        }

        private static void TransactionMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Customer>()
            .HasKey(m => m.Id);

            modelBuilder.Entity<Domain.Customer>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.Customer>()
            .Property(m => m.IBAN)
            .HasMaxLength(24)
            .IsRequired();

            modelBuilder.Entity<Domain.Customer>()
            .Property(m => m.FirstName)
            .IsRequired();

            modelBuilder.Entity<Domain.Customer>()
            .Property(m => m.LastName)
            .IsRequired();
        }

    }
}
