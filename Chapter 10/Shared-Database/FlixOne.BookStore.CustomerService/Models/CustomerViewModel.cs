using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.CustomerService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerViewModel
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public DateTime MemberSince { get; set; }

        public decimal Wallet { get; set; }


        public string FullName { get { return FirstName + " " + LastName; } }
    }

    public class SaveCustomerViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public DateTime MemberSince { get; set; }

        public decimal Wallet { get; set; }


        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
