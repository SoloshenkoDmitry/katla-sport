using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.ProductSupplier
{
    internal sealed class SupplierConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            ToTable("product_suppliers");
            HasKey(i => i.Id);
            Property(i => i.Id).HasColumnName("product_supplier_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.CompanyName).HasColumnName("product_supplier_company_name").HasMaxLength(60).IsRequired();
            Property(i => i.Address).HasColumnName("product_supplier_address").HasMaxLength(300).IsRequired();
            Property(i => i.Phone).HasColumnName("product_supplier_phone").HasMaxLength(20).IsRequired();
            Property(i => i.IsDeleted).HasColumnName("deleted").IsRequired();
            Property(i => i.Created).HasColumnName("created_utc").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(i => i.LastUpdated).HasColumnName("updated_utc").IsRequired();
        }
    }
}