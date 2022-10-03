using Microsoft.EntityFrameworkCore;

namespace NLBank.Infra.Repository.Transaction
{
    public class TransactionDatabaseContext : DbContext
    {
        public TransactionDatabaseContext(DbContextOptions<TransactionDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Domain.Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            TransactionMapping(modelBuilder);
        }

        private static void TransactionMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Transaction>()
            .HasKey(m => m.Id);

            modelBuilder.Entity<Domain.Transaction>()
            .Property(m => m.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.Transaction>()
            .Property(m => m.Details)
            .HasMaxLength(255);

            modelBuilder.Entity<Domain.Transaction>()
            .Property(m => m.OperationId)
            .IsRequired();
        }

    }
}
