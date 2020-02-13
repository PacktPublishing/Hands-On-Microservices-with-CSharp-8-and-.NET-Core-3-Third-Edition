using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.OfferService.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Logo { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; }
        public Address Address { get; set; }

    }
}
