using System;
using System.ComponentModel.DataAnnotations;

namespace KoopSatis.Models.Customer
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        
        public int CustomerId { get; set; }
        
        public string PaymentType { get; set; } // Nakit, Kredi Kartı, Havale, vb.
        
        public decimal Amount { get; set; }
        
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        
        public string ReferenceNumber { get; set; } // Çek no, fiş no, vb.
        
        public int? SalesInvoiceId { get; set; } // İlişkili fatura (opsiyonel)
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public string CreatedByUserId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // İlişki özellikleri
        public virtual Customer Customer { get; set; }
    }
} 