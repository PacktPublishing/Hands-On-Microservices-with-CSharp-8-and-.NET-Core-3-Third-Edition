using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.ProductService.Models
{
    public class ErrorMessage
    {
        public Guid Id { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
    }
}
