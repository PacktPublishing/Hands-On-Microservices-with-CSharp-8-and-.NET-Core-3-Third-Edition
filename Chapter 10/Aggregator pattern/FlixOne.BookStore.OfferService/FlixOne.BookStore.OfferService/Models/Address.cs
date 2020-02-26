using System;

namespace FlixOne.BookStore.OfferService.Models
{
    public class Address
    {
        public Guid Id { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PIN { get; set; }
        public Guid VendorId { get; set; }
    }
}
