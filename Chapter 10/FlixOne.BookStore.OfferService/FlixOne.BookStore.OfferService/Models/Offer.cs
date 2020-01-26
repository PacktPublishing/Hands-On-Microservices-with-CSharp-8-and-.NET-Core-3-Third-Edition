using System;

namespace FlixOne.BookStore.OfferService.Models
{
    public class Offer
    {
        private readonly Deal _deal;
        private readonly Vendor _vendor;


        private Offer() { }

        internal Offer(Deal deal, Vendor vendor)
        {
            _deal = deal;
            _vendor = vendor;

        }

        public Offer Get()
        {
            Offer offer = new Offer();
            if (_deal != null && _deal.StartOn.HasValue)
            {
                offer.OfferCode = _deal.Id.ToString();
                offer.OfferDetails = $"Offer:{_deal.Name}, {_deal.Description}";
                offer.OfferBanner = _deal.Image;
                offer.Discount = _deal.OfferPrice;
                offer.OfferValidTill = _deal.EndOn;
            }
            else
            {
                offer.OfferDetails = "Deal is not available.";
            }
            if (_vendor != null)
            {
                offer.VendorCode = _vendor.Code;
                offer.VendroName = _vendor.Name;
                offer.AccessURL = _vendor.URL;
                offer.VendorLogo = _vendor.Logo;

                if (_vendor.Address != null)
                {
                    var address = _vendor.Address;
                    offer.VendorAddress = $"{address.AddressLine1} {address.AddressLine2}, {address.City}, {address.State}, {address.PIN}";
                }

            }

            return offer;
        }
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