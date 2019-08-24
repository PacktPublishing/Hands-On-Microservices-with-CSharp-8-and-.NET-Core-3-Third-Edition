using System;

namespace FlixOne.BookStore.ProductService.Models
{
    public class ServiceStatus
    {
        public bool Up { get; set; }
        public DateTime UpSince { get; set; }
        public string Version { get; set; }

        public string Message => $"I'm up and running from last {(DateTime.UtcNow - UpSince).Hours} hours.";
    }
}