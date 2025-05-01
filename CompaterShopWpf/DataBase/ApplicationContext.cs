
using CompaterShopWpf.Core.Entities;
using CompaterShopWpf.Windows.OrderHistoryWindow;
using Microsoft.EntityFrameworkCore;

namespace CompaterShopWpf.DataBase;

public class ApplicationContext : DbContext
{
    private static ApplicationContext? _instance;
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    private ApplicationContext() => Database.EnsureCreated();
    
    public static ApplicationContext GetInstance()
    {
        _instance??= new ApplicationContext();
        return _instance;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Config.ConntectionString);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CategoryId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.OrderHistory)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
        
        modelBuilder.Entity<Catalog>()
            .HasMany(c => c.Categories)
            .WithOne(o => o.Catalog)
            .HasForeignKey(o => o.CatalogId);
        
        modelBuilder.Entity<Card>()
            .HasOne(c => c.User)
            .WithMany(u => u.Cards)
            .HasForeignKey(u => u.UserId);
        
        
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => oi.Id);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(io => io.Item)
            .WithMany(i => i.Orders)
            .HasForeignKey(io => io.ItemId);
    }
}