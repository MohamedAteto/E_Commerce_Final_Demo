using E_Commerce_Final_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Final_Demo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();



            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<User>()
               .HasData(

                   new User { UserId = 1, UserFullName = "Ahmed Hassan", UserEmail = "ahmedhassn@gmail.com", UserPasswordHash = "123456A", UserRole = "Customer" },
                   new User { UserId = 2, UserFullName = "Store Admin", UserEmail = "admin@example.com", UserPasswordHash = "123456Adm", UserRole = "Admin" }

                );



            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { CategoryId = 1, CategoryName = "Electronics", CategoryDescription = "Electronic devices and gadgets"  },
                    new Category { CategoryId = 2, CategoryName = "Clothing", CategoryDescription = "Apparel and fashion items" },
                    new Category { CategoryId = 3, CategoryName = "Books", CategoryDescription = "Books and literature" }
                );


            modelBuilder.Entity<Product>()
                .HasData(

                    new Product { ProductId = 1, ProductName = "Smartphone", ProductPrice = 699.99m, ProductDescription = "Latest smartphone with advanced features", StockQuantity = 50, CategoryId = 1 },
                    new Product { ProductId = 2, ProductName = "C++ Fundamentals", ProductPrice = 350.00m, ProductDescription = "Futrue Programming", StockQuantity = 30, CategoryId = 2 }
                );

            modelBuilder.Entity<Order>()
                .HasData(
                    new Order { OrderID = 1, OrderDate = DateTime.Now, UserId = 1 , OrderStatus = "Completed" , TotalAmountOrder = 800.00m },
                    new Order { OrderID = 2, OrderDate = new  DateTime(2026 ,10,10), UserId = 1 , OrderStatus = "Pending", TotalAmountOrder = 1200.00m }
                );

            modelBuilder.Entity<OrderItem>()
                .HasData(

                    new OrderItem { OrderItemID = 1, OrderID = 1, ProductId = 1, OrderItemQuantity = 1, OrderItemPrice = 450.99m },
                    new OrderItem { OrderItemID = 2, OrderID = 2, ProductId = 2, OrderItemQuantity = 1, OrderItemPrice = 350.99m }
                );



        }
    }
}
