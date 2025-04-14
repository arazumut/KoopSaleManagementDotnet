using KoopSatis.Models.Identity;
using KoopSatis.Models.Product;
using KoopSatis.Models.Stock;
using KoopSatis.Models.Sales;
using KoopSatis.Models.Customer;
using KoopSatis.Models.Supplier;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KoopSatis.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Identity
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }

        // Ürün
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        // Stok
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Location> Locations { get; set; }

        // Satış
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesItem> SalesItems { get; set; }

        // Müşteri
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        // Tedarikçi
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // İlişkiler ve kısıtlamalar
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(st => st.Product)
                .WithMany(p => p.StockTransactions)
                .HasForeignKey(st => st.ProductId);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(st => st.Location)
                .WithMany(l => l.StockTransactions)
                .HasForeignKey(st => st.LocationId);

            modelBuilder.Entity<SalesItem>()
                .HasOne(si => si.Product)
                .WithMany(p => p.SalesItems)
                .HasForeignKey(si => si.ProductId);

            modelBuilder.Entity<SalesItem>()
                .HasOne(si => si.SalesInvoice)
                .WithMany(si => si.SalesItems)
                .HasForeignKey(si => si.SalesInvoiceId);

            modelBuilder.Entity<SalesInvoice>()
                .HasOne(si => si.Customer)
                .WithMany(c => c.SalesInvoices)
                .HasForeignKey(si => si.CustomerId);

            modelBuilder.Entity<PaymentTransaction>()
                .HasOne(pt => pt.Customer)
                .WithMany(c => c.PaymentTransactions)
                .HasForeignKey(pt => pt.CustomerId);

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(pi => pi.Supplier)
                .WithMany(s => s.PurchaseInvoices)
                .HasForeignKey(pi => pi.SupplierId);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.PurchaseInvoice)
                .WithMany(pi => pi.PurchaseItems)
                .HasForeignKey(pi => pi.PurchaseInvoiceId);
        }
    }
} 