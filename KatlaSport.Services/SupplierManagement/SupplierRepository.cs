using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.ProductSupplier;
using DbSupplier = KatlaSport.DataAccess.ProductSupplier.Supplier;

namespace KatlaSport.Services.SupplierManagement
{
    public class SupplierRepository : ISupplierRepository
    {
         private readonly IProductSupplierContext _context;

        public SupplierRepository(IProductSupplierContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<SupplierListItem>> GetSuppliersAsync()
        {
            var dbSuppliers = await _context.Suppliers.OrderBy(s => s.Id).ToArrayAsync();
            var suppliers = dbSuppliers.Select(s => Mapper.Map<SupplierListItem>(s)).ToList();

            foreach (SupplierListItem supplier in suppliers)
            {
                supplier.SupplierNoteCount = _context.SupplierNotes.Count(n => n.SupplierId == supplier.Id);
            }

            return suppliers;
        }

        public async Task<Supplier> GetSupplierAsync(int supplierId)
        {
            var dbSuppliers = await _context.Suppliers.Where(s => s.Id == supplierId).ToArrayAsync();
            if (dbSuppliers.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbSupplier, Supplier>(dbSuppliers[0]);
        }

        public async Task SetStatusAsync(int supplierId, bool deletedStatus)
        {
            var dbSuppliers = await _context.Suppliers.Where(s => s.Id == supplierId).ToArrayAsync();

            if (dbSuppliers.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbSupplier = dbSuppliers[0];
            if (dbSupplier.IsDeleted != deletedStatus)
            {
                dbSupplier.IsDeleted = deletedStatus;
                dbSupplier.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Supplier> CreateSupplierAsync(UpdateSupplierRequest createRequest)
        {
            var dbSuppliers = await _context.Suppliers.Where(s => s.CompanyName == createRequest.CompanyName).ToArrayAsync();
            if (dbSuppliers.Length > 0)
            {
                throw new RequestedResourceHasConflictException("company name");
            }

            var dbSupplier = Mapper.Map<UpdateSupplierRequest, DbSupplier>(createRequest);
            _context.Suppliers.Add(dbSupplier);

            await _context.SaveChangesAsync();

            return Mapper.Map<Supplier>(dbSupplier);
        }

        public async Task<Supplier> UpdateSupplierAsync(int supplierId, UpdateSupplierRequest updateRequest)
        {
            var dbSuppliers = await _context.Suppliers.Where(s => s.CompanyName == updateRequest.CompanyName && s.Id != supplierId).ToArrayAsync();
            if (dbSuppliers.Length > 0)
            {
                throw new RequestedResourceHasConflictException("company name");
            }

            dbSuppliers = await _context.Suppliers.Where(s => s.Id == supplierId).ToArrayAsync();
            if (dbSuppliers.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbSupplier = dbSuppliers[0];

            Mapper.Map(updateRequest, dbSupplier);

            await _context.SaveChangesAsync();

            return Mapper.Map<Supplier>(dbSupplier);
        }

        public async Task DeleteSupplierAsync(int supplierId)
        {
            var dbSuppliers = await _context.Suppliers.Where(s => s.Id == supplierId).ToArrayAsync();
            if (dbSuppliers.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbSupplier = dbSuppliers[0];
            if (dbSupplier.IsDeleted == false)
            {
                throw new RequestedResourceHasConflictException();
            }

            _context.Suppliers.Remove(dbSupplier);
            await _context.SaveChangesAsync();
        }
    }
}