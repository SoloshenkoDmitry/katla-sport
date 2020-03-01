using System;

namespace KatlaSport.Services.SupplierManagement
{
    public class Supplier
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}