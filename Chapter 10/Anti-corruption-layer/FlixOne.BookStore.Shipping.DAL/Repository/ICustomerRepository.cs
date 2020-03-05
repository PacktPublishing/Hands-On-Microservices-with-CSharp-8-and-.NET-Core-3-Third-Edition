using FlixOne.BookStore.Shipping.DAL.Models;
using System;
using System.Collections.Generic;

namespace FlixOne.BookStore.Shipping.DAL.Repository
{
    public interface ICustomerRepository
    {
        public IEnumerable<Customer> Get();
        public Customer Get(Guid id);
    }
}
