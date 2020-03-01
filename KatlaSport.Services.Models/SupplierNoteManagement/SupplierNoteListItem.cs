namespace KatlaSport.Services.SupplierNoteManagement
{
    public class SupplierNoteListItem
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Note { get; set; }

        public bool IsDeleted { get; set; }

        public int SupplierId { get; set; }
    }
}