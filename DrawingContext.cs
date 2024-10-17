using Microsoft.EntityFrameworkCore;
using System;


public class DrawingContext : DbContext
{
    public DbSet<Drawing> Drawings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true");
    }
}
