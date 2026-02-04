using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WpfApp3.EF;
using WpfApp3.EF.TableClasses;


public class ApplicationContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Groups> Groups { get; set; }
    public DbSet<Subjects> Subjects { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<GroupSubjects> groupSubjects { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=ksanox");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}