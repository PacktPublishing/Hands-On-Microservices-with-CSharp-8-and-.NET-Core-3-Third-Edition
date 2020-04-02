using System;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BooStore.ReviewService.Models
{
    /// <summary>
    /// Review cache
    /// </summary>
    public class ReviewCache
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public byte[] Value { get; set; }
        public DateTimeOffset ExpiresAtTime { get; set; }

        public long SlidingExpirationInSeconds { get; set; } 
        public DateTimeOffset AbsoluteExpiration { get; set; }
    }
}
