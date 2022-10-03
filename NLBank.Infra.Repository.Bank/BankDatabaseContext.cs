using Microsoft.EntityFrameworkCore;

namespace NLBank.Infra.Repository.Bank
{
    public class BankDatabaseContext : DbContext
    {
        public BankDatabaseContext(DbContextOptions<BankDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Domain.Bank> Banks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TransactionMapping(modelBuilder);
        }

        private static void TransactionMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Bank>()
            .HasKey(m => m.Id);

            modelBuilder.Entity<Domain.Bank>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.Bank>()
            .Property(m => m.BankCode)
            .IsRequired();

            modelBuilder.Entity<Domain.Bank>()
            .Property(m => m.Money)
            .IsRequired();
        }

    }
}
