namespace DAL.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;
    using Microsoft.Extensions.Options;

    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext()
        {
            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory + "appsettings.json");
            this.Configuration = new ConfigurationBuilder()
            .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        }

        public AppDbContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(this.Configuration.GetConnectionString("FlowMeterDatabase"));
        }

        public DbSet<Account> accounts { get; set; }

        public DbSet<Consumer> consumers { get; set; }

        public DbSet<Counter> counters { get; set; }

        public DbSet<Employee> employees { get; set; }

        public DbSet<House> houses { get; set; }

        public DbSet<Service> services { get; set; }
    }
}