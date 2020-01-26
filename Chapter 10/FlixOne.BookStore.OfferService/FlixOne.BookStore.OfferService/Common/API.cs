namespace FlixOne.BookStore.OfferService.Common
{
    public static class API
    {
        public static class Deal
        {
            public static string GetAllDeals(string baseUri) => $"{baseUri}";
            public static string GetDeal(string baseUri, string id) => $"{baseUri}/{id}";
        }

        public static class Vendor
        {
            public static string GetList(string baseUri) => $"{baseUri}";
            public static string GetVendor(string baseUri, string id) => $"{baseUri}/{id}";
        }
    }
}
