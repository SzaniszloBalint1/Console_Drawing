using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

public class DrawingContext : DbContext
{
    public DbSet<Drawing> Drawings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        base.OnConfiguring(optionsBuilder);
       

        var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddUserSecrets<Program>() 
              .Build();

        var connectionString = configuration.GetConnectionString("Balint");
        optionsBuilder.UseSqlServer(connectionString);

    }
}
