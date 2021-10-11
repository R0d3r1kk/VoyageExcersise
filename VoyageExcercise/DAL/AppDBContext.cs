using System;
using VoyageExcercise.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace VoyageExcercise.DAL
{
    /// <summary>
    /// Application Databse Context (voyagedb)
    /// </summary>
    public class AppDBContext:DbContext
    {
        /// <summary>
        /// Local Database
        /// </summary>
        public AppDBContext(DbContextOptions options) :base(options)
        {

        }

        /// <summary>
        /// Transactions Dataset
        /// </summary>
        public DbSet<Transactions> Transactions { get; set; }
        /// <summary>
        /// Payments Dataset
        /// </summary>
        public DbSet<Payments> Payments { get; set; }
        /// <summary>
        /// CServices Dataset
        /// </summary>
        public DbSet<CServices> CServices { get; set; }

        public DbSet<Invoice> Invoice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>()
                .Property(e => e.transaction_id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Payments>()
                .Property(e => e.payment_id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<CServices>()
                .Property(e => e.service_id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Invoice>(
                eb =>
                {
                    eb.HasNoKey();
                    eb.ToView("Invoice_tansaction_payment");
                    eb.Property(v => v.id).HasColumnName("id");
                });
                
        }
    }
}
