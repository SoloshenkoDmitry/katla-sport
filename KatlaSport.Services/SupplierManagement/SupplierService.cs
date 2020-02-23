using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.SupplierManagement
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<List<SupplierListItem>> GetSuppliersAsync()
        {
            return _repository.GetSuppliersAsync();
        }

        public Task<Supplier> GetSupplierAsync(int supplierId)
        {
            return _repository.GetSupplierAsync(supplierId);
        }

        public Task SetStatusAsync(int supplierId, bool deletedStatus)
        {
            return _repository.SetStatusAsync(supplierId, deletedStatus);
        }

        public Task<Supplier> CreateSupplierAsync(UpdateSupplierRequest createRequest)
        {
            return _repository.CreateSupplierAsync(createRequest);
        }

        public Task<Supplier> UpdateSupplierAsync(int supplierId, UpdateSupplierRequest updateRequest)
        {
            return _repository.UpdateSupplierAsync(supplierId, updateRequest);
        }

        public Task DeleteSupplierAsync(int supplierId)
        {
            return _repository.DeleteSupplierAsync(supplierId);
        }
    }
}