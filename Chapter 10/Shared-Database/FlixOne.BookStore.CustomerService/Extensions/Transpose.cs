using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.CustomerService.Extensions
{
    public static class Transpose
    {
        public static Models.Customer ToModel(this Models.CustomerViewModel vm)
        {
            return new Models.Customer
            {
                Id =vm.CustomerId,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                MemberSince = vm.MemberSince,
                Wallet = vm.Wallet
            };
        }
        public static IEnumerable<Models.Customer> ToModel(this IEnumerable<Models.CustomerViewModel> vm) => vm.Select(ToModel);

        public static Models.CustomerViewModel ToViewModel(this Models.Customer model)
        {
            return new Models.CustomerViewModel
            {
                CustomerId = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MemberSince = model.MemberSince,
                Wallet = model.Wallet
            };
        }
        public static IEnumerable<Models.CustomerViewModel> ToViewModel(this IEnumerable<Models.Customer> model) => model.Select(ToViewModel);
    }
}
