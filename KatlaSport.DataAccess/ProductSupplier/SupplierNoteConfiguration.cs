using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KatlaSport.DataAccess.ProductSupplier
{
    internal sealed class SupplierNoteConfiguration : EntityTypeConfiguration<SupplierNote>
    {
        public SupplierNoteConfiguration()
        {
            ToTable("product_supplier_notes");
            HasKey(i => i.Id);
            HasRequired(i => i.Supplier).WithMany(i => i.Notes).HasForeignKey(i => i.SupplierId);
            Property(i => i.Id).HasColumnName("product_supplier_notes_id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.ParentId).HasColumnName("product_supplier_notes_parent_id");
            Property(i => i.Note).HasColumnName("product_supplier_notes_note").HasMaxLength(500);
            Property(i => i.SupplierId).HasColumnName("product_supplier_id");
            Property(i => i.IsDeleted).HasColumnName("deleted").IsRequired();
            Property(i => i.Created).HasColumnName("created_utc").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(i => i.LastUpdated).HasColumnName("updated_utc").IsRequired();
        }
    }
}