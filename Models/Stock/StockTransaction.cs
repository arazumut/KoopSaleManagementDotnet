using System;
using System.ComponentModel.DataAnnotations;
using KoopSatis.Models.Product;

namespace KoopSatis.Models.Stock
{
    public class StockTransaction
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        
        public int LocationId { get; set; }
        
        [Required]
        public string TransactionType { get; set; } // Giriş, Çıkış, Transfer, vb.
        
        public decimal Quantity { get; set; }
        
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public string ReferenceCode { get; set; } // Satış veya alım fatura numarası
        
        public int? ReferenceId { get; set; } // İlgili satış veya alım ID'si
        
        public string CreatedByUserId { get; set; }
        
        // İlişki özellikleri
        public virtual Product.Product Product { get; set; }
        
        public virtual Location Location { get; set; }
    }
} 