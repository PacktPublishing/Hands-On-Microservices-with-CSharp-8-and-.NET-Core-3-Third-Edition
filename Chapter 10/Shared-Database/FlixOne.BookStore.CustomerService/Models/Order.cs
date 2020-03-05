using System;

namespace FlixOne.BookStore.CustomerService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
    }
}
