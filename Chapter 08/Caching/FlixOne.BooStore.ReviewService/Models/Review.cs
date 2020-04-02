using System;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BooStore.ReviewService.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        public string Comment { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
