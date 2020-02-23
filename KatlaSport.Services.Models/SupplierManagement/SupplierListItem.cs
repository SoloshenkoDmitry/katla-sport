namespace KatlaSport.Services.SupplierManagement
{
    public class SupplierListItem
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public bool IsDeleted { get; set; }

        public int SupplierNoteCount { get; set; }
    }
}