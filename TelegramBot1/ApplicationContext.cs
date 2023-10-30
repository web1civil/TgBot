using Microsoft.EntityFrameworkCore;

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
        optionsBuilder.UseSqlServer(@"Server=127.0.0.1;Database=TelegramBotData;User ID = sa; Password=qwerty!123;TrustServerCertificate=true;");
    }
}