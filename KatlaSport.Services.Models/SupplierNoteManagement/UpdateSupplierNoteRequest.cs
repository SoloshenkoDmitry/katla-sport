using FluentValidation.Attributes;

namespace KatlaSport.Services.SupplierNoteManagement
{
    [Validator(typeof(UpdateSupplierNoteRequestValidator))]
    public class UpdateSupplierNoteRequest
    {
        public int? ParentId { get; set; }

        public string Note { get; set; }

        public int SupplierId { get; set; }
    }
}