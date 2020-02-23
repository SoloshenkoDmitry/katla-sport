using FluentValidation;

namespace KatlaSport.Services.SupplierManagement
{
    public class UpdateSupplierRequestValidator : AbstractValidator<UpdateSupplierRequest>
    {
        public UpdateSupplierRequestValidator()
        {
            RuleFor(r => r.CompanyName).Length(4, 60);
            RuleFor(r => r.Address).Length(0, 300);
            RuleFor(r => r.Phone).Length(0, 20);
        }
    }
}