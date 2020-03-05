using FlixOne.BookStore.CustomerService.Contexts;
using FlixOne.BookStore.CustomerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.CustomerService.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> Get()
        {
            return _context.Customers.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer Get(Guid customerId)
        {
            return _context.Customers.Where(c => c.Id == customerId).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        public void Add(Customer customer)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
}
