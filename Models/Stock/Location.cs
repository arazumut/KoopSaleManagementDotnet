using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoopSatis.Models.Stock
{
    public class Location
    {
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(200)]
        public string Address { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        // İlişki özellikleri
        public virtual ICollection<StockTransaction> StockTransactions { get; set; } 
            = new List<StockTransaction>();
    }
} 