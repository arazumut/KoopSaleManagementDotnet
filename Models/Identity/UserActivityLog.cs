using System;

namespace KoopSatis.Models.Identity
{
    public class UserActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ActivityType { get; set; } // Login, Logout, Create, Update, Delete, etc.
        public string Description { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string EntityName { get; set; } // Hangi tablo/varlık üzerinde işlem yapıldı
        public string? EntityId { get; set; } // İlgili varlığın Id'si
        
        // İlişki özellikleri
        public virtual ApplicationUser User { get; set; }
    }
} 