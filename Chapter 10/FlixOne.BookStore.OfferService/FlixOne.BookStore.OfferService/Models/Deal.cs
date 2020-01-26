using System;

namespace FlixOne.BookStore.OfferService.Models
{
    public class Deal
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public decimal OfferPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? StartOn { get; set; }
        public DateTime? EndOn { get; set; }
    }
}
