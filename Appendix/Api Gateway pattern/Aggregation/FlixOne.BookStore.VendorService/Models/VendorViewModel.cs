using System;

namespace FlixOne.BookStore.VendorService.Models
{
    public class VendorViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Logo { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; }

        public AddressViewModel Address { get; set; }
    }

    public class AddressViewModel
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