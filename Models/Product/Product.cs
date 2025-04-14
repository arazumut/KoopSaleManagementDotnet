using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KoopSatis.Models.Stock;
using KoopSatis.Models.Sales;
using KoopSatis.Models.Supplier;

namespace KoopSatis.Models.Product
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(20)]
        public string Barcode { get; set; }
        
        [MaxLength(50)]
        public string SKU { get; set; } // Stok Kodu
        
        public int CategoryId { get; set; }
        
        [MaxLength(50)]
        public string Unit { get; set; } // Kg, Adet, Litre, vb.
        
        public int? ShelfLife { get; set; } // Raf ömrü (gün cinsinden)
        
        public decimal MinimumStock { get; set; } = 0;
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        
        public decimal PurchasePrice { get; set; } = 0; // Alış fiyatı
        
        public decimal SalePrice { get; set; } = 0; // Satış fiyatı
        
        public decimal TaxRate { get; set; } = 0; // KDV oranı
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Stok hesaplama için yardımcı özellik (veritabanında saklanmaz)
        public decimal CurrentStock { get; set; }
        
        // İlişki özellikleri
        public virtual ProductCategory Category { get; set; }
        
        public virtual ICollection<StockTransaction> StockTransactions { get; set; } 
            = new List<StockTransaction>();
            
        public virtual ICollection<SalesItem> SalesItems { get; set; } 
            = new List<SalesItem>();
            
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } 
            = new List<PurchaseItem>();
    }
} 