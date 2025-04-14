using System;
using System.ComponentModel.DataAnnotations;
using KoopSatis.Models.Product;

namespace KoopSatis.Models.Supplier
{
    public class PurchaseItem
    {
        public int Id { get; set; }
        
        public int PurchaseInvoiceId { get; set; }
        
        public int ProductId { get; set; }
        
        public decimal Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TaxRate { get; set; } = 0;
        
        public decimal TaxAmount { get; set; } = 0;
        
        public decimal DiscountRate { get; set; } = 0;
        
        public decimal DiscountAmount { get; set; } = 0;
        
        public decimal LineTotal { get; set; } = 0; // (Quantity * UnitPrice) - DiscountAmount + TaxAmount
        
        // İade bilgileri
        public bool IsReturned { get; set; } = false;
        
        public decimal ReturnedQuantity { get; set; } = 0;
        
        public string ReturnReason { get; set; }
        
        public DateTime? ReturnDate { get; set; }
        
        // İlişki özellikleri
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
        
        public virtual Product.Product Product { get; set; }
    }
} 