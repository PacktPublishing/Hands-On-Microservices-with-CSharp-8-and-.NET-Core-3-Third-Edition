using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.StoreService.Models
{
    public class BookType:BaseEntity
    {
        public BookType()
        {
            Categories = new HashSet<Category>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
