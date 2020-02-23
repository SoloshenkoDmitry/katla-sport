using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.SupplierManagement
{
    public interface ISupplierService
    {
        Task<List<SupplierListItem>> GetSuppliersAsync();

        Task<Supplier> GetSupplierAsync(int supplierId);

        Task SetStatusAsync(int supplierId, bool deletedStatus);

        Task<Supplier> CreateSupplierAsync(UpdateSupplierRequest createRequest);

        Task<Supplier> UpdateSupplierAsync(int supplierId, UpdateSupplierRequest updateRequest);

        Task DeleteSupplierAsync(int supplierId);
    }
}