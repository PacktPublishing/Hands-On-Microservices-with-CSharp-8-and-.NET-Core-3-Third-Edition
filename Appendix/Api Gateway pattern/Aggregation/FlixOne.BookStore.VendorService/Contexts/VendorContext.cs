using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.VendorService.Contexts
{
    public class VendorContext : DbContext
    {
        public VendorContext(DbContextOptions<VendorContext> options)
            : base(options)
        {
        }

        public VendorContext()
        {
        }
        public DbSet<Models.Vendor> Vendors { get; set; }
        public DbSet<Models.Address> Addresses { get; set; }
    }
}