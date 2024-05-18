using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using wallet_microservice_playtomic_dotnet._1.Domain.Entities;

namespace wallet_microservice_playtomic_dotnet._1.Domain.DatabaseContext
{
    public class DbContextData : DbContext
    {
        public DbContextData(DbContextOptions<DbContextData> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            base.OnConfiguring(optionsBuilder);
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WalletEntity>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<WalletEntity>()
            .Property(e => e.Updated)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(modelBuilder);
        }

        

        public DbSet<TransactionEntity> Transaction { get; set; }
        public DbSet<WalletEntity> Wallet { get; set; }
        public DbSet<StripePaymentEntity> StripePayment { get; set; }



    }
}

