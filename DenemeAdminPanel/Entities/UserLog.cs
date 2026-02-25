using System;

namespace DenemeAdminPanel.Entities
{
    public class UserLog
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string IpAddress { get; set; }
    }
} 