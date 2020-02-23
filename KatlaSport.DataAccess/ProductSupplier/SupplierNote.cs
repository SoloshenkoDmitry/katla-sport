using System;

namespace KatlaSport.DataAccess.ProductSupplier
{
    public class SupplierNote
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Note { get; set; }

        public bool IsDeleted { get; set; }

        public int SupplierId { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}