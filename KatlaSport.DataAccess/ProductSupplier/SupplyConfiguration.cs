using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.ProductSupplier
{
    public class SupplyConfiguration : EntityTypeConfiguration<Supply>
    {
        public SupplyConfiguration()
        {
            ToTable("product_supplies");
            HasKey(i => new { i.SupplierId, i.CatalogueProductId });
            HasRequired(i => i.Supplier).WithMany(i => i.Supplies).HasForeignKey(i => i.SupplierId);
            HasRequired(i => i.Product).WithMany(i => i.Supplies).HasForeignKey(i => i.CatalogueProductId);
            Property(i => i.SupplierId).HasColumnName("supplier_id");
            Property(i => i.CatalogueProductId).HasColumnName("catalogue_product_id");
        }
    }
}