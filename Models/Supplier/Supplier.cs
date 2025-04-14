using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoopSatis.Models.Supplier
{
    public class Supplier
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
        
        [MaxLength(100)]
        public string ContactPerson { get; set; }
        
        [MaxLength(20)]
        public string TaxNumber { get; set; }
        
        [MaxLength(100)]
        public string TaxOffice { get; set; }
        
        [MaxLength(50)]
        public string BankName { get; set; }
        
        [MaxLength(30)]
        public string BankAccount { get; set; }
        
        [MaxLength(30)]
        public string IBAN { get; set; }
        
        public decimal CurrentBalance { get; set; } = 0; // Tedarikçiye olan borç
        
        [MaxLength(500)]
        public string Notes { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        // İlişki özellikleri
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } 
            = new List<PurchaseInvoice>();
    }
} 