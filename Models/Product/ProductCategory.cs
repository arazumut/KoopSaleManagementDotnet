using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoopSatis.Models.Product
{
    public class ProductCategory
    {
        public int Id { get; set; }
        
        [Required, MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
        
        // İlişki özellikleri
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
} 