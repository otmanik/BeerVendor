using BeerVendor.Data;
using BeerVendor.Data.Repositories;
using BeerVendor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BeerVendor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IBeerService, BeerService>();
            builder.Services.AddTransient<IWholesalerStockService, WholesalerStockService>();
            builder.Services.AddScoped<IBeerRepository, BeerRepository>();
            builder.Services.AddScoped<IWholesalerRepository, WholesalerRepository>();

            // Build the service provider
            var serviceProvider = builder.Services.BuildServiceProvider();

            // Resolve the service from the provider
            var beerService = serviceProvider.GetService<IBeerService>();
            var stockService = serviceProvider.GetService<IWholesalerStockService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}