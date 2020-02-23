using KatlaSport.DataAccess.ProductCatalogue;

namespace KatlaSport.DataAccess.ProductSupplier
{
    public class Supply
    {
        public int SupplierId { get; set; }

        public int CatalogueProductId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual CatalogueProduct Product { get; set; }
    }
}