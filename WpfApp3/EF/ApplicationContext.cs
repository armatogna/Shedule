using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WpfApp3.EF;


public class ApplicationContext : DbContext
{
    public DbSet<Groups> Groups { get; set; } = null!;
    public DbSet<Subjects> Subjects { get; set; } = null!;
    public DbSet<Lessons> Lessons { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=ksanox");
    }
    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Groups>().Property("Id").HasField("id");
        modelBuilder.Entity<Groups>().Property("Age").HasField("age");
        
    }*/
}