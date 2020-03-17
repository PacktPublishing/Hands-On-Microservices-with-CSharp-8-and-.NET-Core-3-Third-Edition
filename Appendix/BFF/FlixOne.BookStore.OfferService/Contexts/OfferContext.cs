using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.VendorService.Contexts
{
    public class OfferContext : DbContext
    {
        public OfferContext(DbContextOptions<OfferContext> options)
            : base(options)
        {
        }

        public OfferContext()
        {
        }
        public DbSet<Models.Offer> Offers { get; set; }
    }
}