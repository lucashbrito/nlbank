using Microsoft.EntityFrameworkCore;

namespace NLBank.Infra.Repository.Account
{
    public class AccountDatabaseContext : DbContext
    {
        public AccountDatabaseContext(DbContextOptions<AccountDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Domain.Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TransactionMapping(modelBuilder);
        }

        private static void TransactionMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Account>()
            .HasKey(m => m.Id);

            modelBuilder.Entity<Domain.Account>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.Account>()
            .Property(m => m.IBAN)
            .HasMaxLength(24)
            .IsRequired();

            modelBuilder.Entity<Domain.Account>()
            .Property(m => m.Money)
            .IsRequired();


            modelBuilder.Entity<Domain.Account>()
            .Property(m => m.CustomerId)
            .IsRequired();
        }

    }
}
