using FluentValidation;

namespace KatlaSport.Services.SupplierNoteManagement
{
    public class UpdateSupplierNoteRequestValidator : AbstractValidator<UpdateSupplierNoteRequest>
    {
        public UpdateSupplierNoteRequestValidator()
        {
            RuleFor(r => r.Note).Length(0, 500);
        }
    }
}