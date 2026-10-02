using Microsoft.EntityFrameworkCore;
using implementation.Models;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    public DbSet<Pedido> Pedidos { get; set; }

}