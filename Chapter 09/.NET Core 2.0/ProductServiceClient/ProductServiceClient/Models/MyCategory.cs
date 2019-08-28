using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServiceClient.Models
{
    public class MyCategory
    {
        public MyCategory()
        {
            MyProducts = new List<MyProduct>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<MyProduct> MyProducts { get; set; }
    }
}
