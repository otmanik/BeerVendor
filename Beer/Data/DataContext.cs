using BeerVendor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;

namespace BeerVendor.Data
{
    public class DataContext : DbContext
    {
        private readonly DbContextOptions<DataContext> _options;
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            _options = options;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            }
        }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerStock> WholesalerStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the Brewery entity
            modelBuilder.Entity<Brewery>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Brewery>()
                .Property(b => b.Name)
                .IsRequired();

            // Define the Beer entity
            modelBuilder.Entity<Beer>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Beer>()
                .Property(b => b.Name)
                .IsRequired();

            modelBuilder.Entity<Beer>()
                .Property(b => b.AlcoholContent)
                .IsRequired();

            modelBuilder.Entity<Beer>()
                .Property(b => b.Price)
                .IsRequired();

            modelBuilder.Entity<Beer>()
                .HasOne(b => b.Brewery)
                .WithMany()
                .HasForeignKey(b => b.BreweryId);

            // Define the Wholesaler entity
            modelBuilder.Entity<Wholesaler>()
                .HasKey(w => w.Id);

            modelBuilder.Entity<Wholesaler>()
                .Property(w => w.Name)
                .IsRequired();

            // Define the WholesalerStock entity
            modelBuilder.Entity<WholesalerStock>()
                .HasKey(ws => new { ws.WholesalerId, ws.BeerId });

            modelBuilder.Entity<WholesalerStock>()
                .HasOne(ws => ws.Wholesaler)
                .WithMany()
                .HasForeignKey(ws => ws.WholesalerId);

            modelBuilder.Entity<WholesalerStock>()
                .HasOne(ws => ws.Beer)
                .WithMany()
                .HasForeignKey(ws => ws.BeerId);

            modelBuilder.Entity<WholesalerStock>()
                .Property(ws => ws.Quantity)
                .IsRequired();

            // Add seed data
            modelBuilder.Entity<Brewery>().HasData(
                new Brewery { Id = 1, Name = "Abbaye de Leffe" }
            );

            modelBuilder.Entity<Beer>().HasData(
                new Beer { Id = 1, Name = "Leffe Blonde", AlcoholContent = 6.6, Price = 2.20M, BreweryId = 1 }
            );

            modelBuilder.Entity<Wholesaler>().HasData(
                new Wholesaler { Id = 1, Name = "GeneDrinks" }
            );

            modelBuilder.Entity<WholesalerStock>().HasData(
                new WholesalerStock { WholesalerId = 1, BeerId = 1, Quantity = 10 }
            );
        }

    }
}
