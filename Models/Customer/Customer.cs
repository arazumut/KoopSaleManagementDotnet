using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KoopSatis.Models.Sales;

namespace KoopSatis.Models.Customer
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Address { get; set; }
        
        [MaxLength(20)]
        public string Phone { get; set; }
        
        [MaxLength(100)]
        public string Email { get; set; }
        
        [MaxLength(20)]
        public string TaxNumber { get; set; }
        
        [MaxLength(100)]
        public string TaxOffice { get; set; }
        
        public decimal CurrentBalance { get; set; } = 0; // Güncel cari bakiye
        
        public decimal CreditLimit { get; set; } = 0; // Kredi limiti
        
        [MaxLength(500)]
        public string Notes { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        // İlişki özellikleri
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; } 
            = new List<SalesInvoice>();
            
        public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } 
            = new List<PaymentTransaction>();
    }
} 