using FluentValidation.Attributes;

namespace KatlaSport.Services.SupplierManagement
{
    [Validator(typeof(UpdateSupplierRequestValidator))]
    public class UpdateSupplierRequest
    {
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }
    }
}