using System;

namespace FlixOne.BookStore.Web.Models
{
    public class OfferViewModel
    {
        public string OfferCode { get; set; }
        public string OfferDetails { get; set; }
        public string OfferBanner { get; set; }
        public decimal Discount { get; set; }
        public DateTime? OfferValidTill { get; set; }

        public string VendorCode { get; set; }
        public string VendroName { get; set; }
        public string AccessURL { get; set; }
        public string VendorLogo { get; set; }
        public string VendorAddress { get; set; }
    }
}
