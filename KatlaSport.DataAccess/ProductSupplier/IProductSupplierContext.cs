namespace KatlaSport.DataAccess.ProductSupplier
{
    public interface IProductSupplierContext : IAsyncEntityStorage
    {
        IEntitySet<Supplier> Suppliers { get; }

        IEntitySet<SupplierNote> SupplierNotes { get; }

        IEntitySet<Supply> Supplies { get; }
    }
}