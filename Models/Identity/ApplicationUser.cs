using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace KoopSatis.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ProfilePictureUrl { get; set; }
        public string EmployeePosition { get; set; }
        public bool IsActive { get; set; } = true;
        
        // İlişki özellikleri
        public virtual ICollection<UserActivityLog> ActivityLogs { get; set; }
            = new List<UserActivityLog>();
    }
} 