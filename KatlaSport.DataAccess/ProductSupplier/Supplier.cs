using System;
using System.Collections.Generic;

namespace KatlaSport.DataAccess.ProductSupplier
{
    public class Supplier
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ICollection<SupplierNote> Notes { get; set; }

        public virtual ICollection<Supply> Supplies { get; set; }
    }
}