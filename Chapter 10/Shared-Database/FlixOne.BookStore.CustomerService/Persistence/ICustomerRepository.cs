using FlixOne.BookStore.CustomerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.CustomerService.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> Get();
        public Customer Get(Guid customerId);
        public void Add(Customer customer);
    }
}
