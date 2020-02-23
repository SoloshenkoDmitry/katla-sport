using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatlaSport.Services.SupplierNoteManagement
{
    public interface ISupplierNoteService
    {
        Task<List<SupplierNoteListItem>> GetSupplierNotesAsync();

        Task<SupplierNote> GetSupplierNoteAsync(int id);

        Task<List<SupplierNoteListItem>> GetSupplierNotesAsync(int noteId);

        Task<SupplierNote> CreateSupplierNoteAsync(UpdateSupplierNoteRequest createRequest);

        Task<SupplierNote> UpdateSupplierNoteAsync(int supplierNoteId, UpdateSupplierNoteRequest updateRequest);

        Task SetStatusAsync(int supplierNoteId, bool deletedStatus);

        Task DeleteSupplierNoteAsync(int supplierNoteId);
    }
}