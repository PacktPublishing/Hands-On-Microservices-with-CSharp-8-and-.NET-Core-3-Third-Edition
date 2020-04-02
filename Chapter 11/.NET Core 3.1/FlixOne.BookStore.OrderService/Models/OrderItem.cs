using System;
using System.ComponentModel.DataAnnotations;

namespace FlixOne.BookStore.OrderService.Models
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public int QtyOrdered { get; set; }

        public decimal ItemTotal
        {
            get
            {
                if (QtyOrdered < 1)
                    throw new Exception("Invalid qty.");

                return QtyOrdered * UnitPrice;
            }
        }

        public Guid OrderId { get; set; }
        public OrderInfo OrderInfo { get; set; }
    }
}