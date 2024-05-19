using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using wallet_microservice_dotnet._1.Domain.Entities;

namespace wallet_microservice_dotnet._1.Domain.DatabaseContext
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
            // Configuración de la clave primaria para la entidad Table
            modelBuilder.Entity<WalletEntity>()
                .HasKey(x => x.Id)
                .HasName("PK_dbo.Wallet.Id");

            // Configuración de la columna Id como identidad
            modelBuilder.Entity<WalletEntity>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<WalletEntity>()
            .Property(e => e.Updated)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<WalletTransactionsEntity>()
            .Property(t => t.TransactionType)
            .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

        

        public DbSet<WalletTransactionsEntity> WalletTransaction { get; set; }
        public DbSet<WalletEntity> Wallet { get; set; }



    }
}

