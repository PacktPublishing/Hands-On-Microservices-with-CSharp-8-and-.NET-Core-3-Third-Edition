using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.VendorService.Common
{
    public static class Transpose
    {
        public static Models.Vendor ToModel(this Models.VendorViewModel vm)
        {
            var vendor = new Models.Vendor
            {
                Id = new System.Guid(vm.Id),
                Active = vm.Active,
                AddedOn = vm.AddedOn,
                Code = vm.Code,
                Description = vm.Description,
                Logo = vm.Logo,
                Name = vm.Name,
                URL = vm.URL
            };
            if (vm.Address != null)
            {
                var address = vm.Address;
                vendor.Addresses = new List<Models.Address>
                {
                new Models.Address
                {
                    Id = address.Id,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    City = address.City,
                    Country = address.Country,
                    State = address.State,
                    PIN = address.PIN,
                    VendorId = address.VendorId
                }
                };
            }
            return vendor;
        }

        public static IEnumerable<Models.Vendor> ToModel(this IEnumerable<Models.VendorViewModel> vm) => vm.Select(item => item.ToModel()).ToList();

        public static Models.VendorViewModel ToViewModel(this Models.Vendor model)
        {
            var vendorVM = new Models.VendorViewModel
            {
                Id = model.Id.ToString(),
                Active = model.Active,
                AddedOn = model.AddedOn,
                Code = model.Code,
                Description = model.Description,
                Logo = model.Logo,
                Name = model.Name,
                URL = model.URL
            };

            if (model.Addresses.Any())
            {
                var address = model.Addresses.FirstOrDefault();

                vendorVM.Address = new Models.AddressViewModel
                {
                    Id = address.Id,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    City = address.City,
                    Country = address.Country,
                    State = address.State,
                    PIN = address.PIN,
                    VendorId = address.VendorId
                };
            }

            return vendorVM;
        }

        public static IEnumerable<Models.VendorViewModel> ToViewModel(this IEnumerable<Models.Vendor> model) => model.Select(item => item.ToViewModel()).ToList();
    }
}