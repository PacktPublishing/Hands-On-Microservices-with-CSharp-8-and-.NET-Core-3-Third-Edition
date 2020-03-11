using FlixOne.BookStore.OrderService.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlixOne.BookStore.OrderService.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderRepository:IOrderRepository
    {
        private readonly OrderContext _context;
        public OrderRepository(OrderContext context) => _context = context;

        public IEnumerable<Models.Order> List() => _context.Orders.Include(o => o.Items).ToList();

        public Models.Order Get(Guid id) => _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id);
        public void Add(Models.Order order)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var customer = _context.Customers.Where(c => c.Id == order.CustomerId).FirstOrDefault();
                    var walletBalance = customer.Wallet;
                    if(walletBalance > 0)
                    {
                        if(walletBalance >= order.NetPay)
                        {
                            order.NetPay = 0M; //Deduct total payment from Wallet Balance
                            customer.Wallet = walletBalance - order.NetPay; //Deduct amount from wallet and save the remainig amount
                        }
                        else
                        {
                            order.NetPay = walletBalance - order.NetPay; //partially deduct amount from wallet
                            customer.Wallet = 0M; // empty the wallet
                        }
                        //Update customer to reflect new/updated Wallet balance
                        _context.Customers.Update(customer);
                    }
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public void AddOrderItem(Models.OrderItem item)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var orderItemForProduct = _context.OrderItems.Where(o => o.ProductId == item.ProductId).SingleOrDefault(); //There shoudl always single record for specific product
                    if (orderItemForProduct != null)
                    {
                        if (item.Discount < 0)
                        {
                            //discount can't be -ve leave it
                            //if there is specific case then we can through an exception
                            //and notify the user
                        }
                        orderItemForProduct.Discount = item.Discount;
                        if (item.Qty > 0)
                        {
                            orderItemForProduct.Qty += item.Qty;
                        }
                        orderItemForProduct.DateModified = DateTime.UtcNow;
                        _context.OrderItems.Update(orderItemForProduct);
                    }
                    else
                    {
                        var orderItem = _context.OrderItems.OrderBy(o => o.Sequence).LastOrDefault();
                        item.Sequence = (orderItem != null) ? orderItem.Sequence + 1 : 1;
                        _context.OrderItems.Add(item);
                    }

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
