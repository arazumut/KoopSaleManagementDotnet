using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KoopSatis.Models.Customer;

namespace KoopSatis.Models.Sales
{
    public class SalesInvoice
    {
        public int Id { get; set; }
        
        public string InvoiceNumber { get; set; }
        
        public int CustomerId { get; set; }
        
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        
        public string PaymentStatus { get; set; } // Ödendi, Kısmi Ödeme, Ödenmedi
        
        public decimal TotalAmount { get; set; } = 0;
        
        public decimal TaxAmount { get; set; } = 0;
        
        public decimal DiscountAmount { get; set; } = 0;
        
        public decimal PaidAmount { get; set; } = 0;
        
        [MaxLength(500)]
        public string Notes { get; set; }
        
        public bool IsCancelled { get; set; } = false;
        
        public string CancellationReason { get; set; }
        
        public string CreatedByUserId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        // İlişki özellikleri
        public virtual Customer.Customer Customer { get; set; }
        
        public virtual ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
    }
} 