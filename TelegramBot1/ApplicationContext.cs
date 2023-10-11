using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ApplicationContext : DbContext
{
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public ApplicationContext()
    {
       
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=appdb1;Trusted_Connection=True;");
    }
}