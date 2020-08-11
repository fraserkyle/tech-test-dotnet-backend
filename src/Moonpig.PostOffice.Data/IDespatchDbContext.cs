namespace Moonpig.PostOffice.Data
{
    using System.Linq;

    public interface IDespatchDbContext
    {
        IQueryable<Supplier> Suppliers { get; }

        IQueryable<Product> Products { get; }

        IQueryable<BlockedDate> BlockedDates { get; }
    }
}
