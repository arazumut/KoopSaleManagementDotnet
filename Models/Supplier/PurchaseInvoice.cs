using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoopSatis.Models.Supplier
{
    public class PurchaseInvoice
    {
        public int Id { get; set; }
        
        public string InvoiceNumber { get; set; }
        
        public int SupplierId { get; set; }
        
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        
        public string PaymentStatus { get; set; } // Ödendi, Kısmi Ödeme, Ödenmedi
        
        public decimal TotalAmount { get; set; } = 0;
        
        public decimal TaxAmount { get; set; } = 0;
        
        public decimal DiscountAmount { get; set; } = 0;
        
        public decimal PaidAmount { get; set; } = 0;
        
        public DateTime? DueDate { get; set; } // Son ödeme tarihi
        
        [MaxLength(500)]
        public string Notes { get; set; }
        
        public bool IsCancelled { get; set; } = false;
        
        public string CancellationReason { get; set; }
        
        public string CreatedByUserId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        // İlişki özellikleri
        public virtual Supplier Supplier { get; set; }
        
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
    }
} 