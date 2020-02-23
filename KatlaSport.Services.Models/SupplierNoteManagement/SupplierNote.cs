using System;

namespace KatlaSport.Services.SupplierNoteManagement
{
    public class SupplierNote : SupplierNoteListItem
    {
        public DateTime LastUpdated { get; set; }

        public int SupplierId { get; set; }
    }
}