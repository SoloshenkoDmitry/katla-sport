namespace KatlaSport.DataAccess.ProductSupplier
{
    internal sealed class ProductSupplierContext : DomainContextBase<ApplicationDbContext>, IProductSupplierContext
    {
        public ProductSupplierContext(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEntitySet<Supplier> Suppliers => GetDbSet<Supplier>();

        public IEntitySet<SupplierNote> SupplierNotes => GetDbSet<SupplierNote>();

        public IEntitySet<Supply> Supplies => GetDbSet<Supply>();
    }
}