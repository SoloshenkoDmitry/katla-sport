using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KatlaSport.DataAccess;
using KatlaSport.DataAccess.ProductSupplier;
using DbSupplierNote = KatlaSport.DataAccess.ProductSupplier.SupplierNote;

namespace KatlaSport.Services.SupplierNoteManagement
{
    public class SupplierNoteService : ISupplierNoteService
    {
        private readonly IProductSupplierContext _context;

        public SupplierNoteService(IProductSupplierContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<List<SupplierNoteListItem>> GetSupplierNotesAsync()
        {
            var dbSupplierNotes = await _context.SupplierNotes.OrderBy(n => n.Id).ToArrayAsync();
            var supplierNotes = dbSupplierNotes.Select(n => Mapper.Map<SupplierNoteListItem>(n)).ToList();
            return supplierNotes;
        }

        public async Task<SupplierNote> GetSupplierNoteAsync(int idSupplierNote)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.Id == idSupplierNote).ToArrayAsync();
            if (dbSupplierNotes.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            return Mapper.Map<DbSupplierNote, SupplierNote>(dbSupplierNotes[0]);
        }

        public async Task<List<SupplierNoteListItem>> GetSupplierNotesAsync(int parentId)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.ParentId == parentId).ToArrayAsync();

            if (dbSupplierNotes.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var supplierNotes = dbSupplierNotes.Select(n => Mapper.Map<SupplierNoteListItem>(n)).ToList();

            List<SupplierNoteListItem> result = new List<SupplierNoteListItem>();
            foreach (var note in supplierNotes)
            {
                result.Add(note);
                await GetChildren(result, note.Id);
            }

            return result;
        }

        public async Task<SupplierNote> CreateSupplierNoteAsync(UpdateSupplierNoteRequest createRequest)
        {
            var dbSupplierNote = Mapper.Map<UpdateSupplierNoteRequest, DbSupplierNote>(createRequest);
            _context.SupplierNotes.Add(dbSupplierNote);

            await _context.SaveChangesAsync();

            return Mapper.Map<SupplierNote>(dbSupplierNote);
        }

        public async Task<SupplierNote> UpdateSupplierNoteAsync(int supplierNoteId, UpdateSupplierNoteRequest updateRequest)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.Id == supplierNoteId).ToArrayAsync();
            if (dbSupplierNotes.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbSupplierNote = dbSupplierNotes[0];

            Mapper.Map(updateRequest, dbSupplierNote);

            await _context.SaveChangesAsync();

            return Mapper.Map<SupplierNote>(dbSupplierNote);
        }

        public async Task SetStatusAsync(int supplierNoteId, bool deletedStatus)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.Id == supplierNoteId).ToArrayAsync();
            if (dbSupplierNotes.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbSupplierNote = dbSupplierNotes[0];
            if (dbSupplierNote.IsDeleted != deletedStatus)
            {
                dbSupplierNote.IsDeleted = deletedStatus;
                dbSupplierNote.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            await UpdateStatusSupplierNoteChildrenAsync(supplierNoteId, deletedStatus);
        }

        public async Task DeleteSupplierNoteAsync(int supplierNoteId)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.Id == supplierNoteId).ToArrayAsync();
            if (dbSupplierNotes.Length == 0)
            {
                throw new RequestedResourceNotFoundException();
            }

            var dbSupplierNote = dbSupplierNotes[0];
            if (dbSupplierNote.IsDeleted == false)
            {
                throw new RequestedResourceHasConflictException();
            }

            _context.SupplierNotes.Remove(dbSupplierNote);
            await _context.SaveChangesAsync();

            await DeleteSupplierNoteChildrenAsync(dbSupplierNote.Id);
        }

        private async Task GetChildren(List<SupplierNoteListItem> children, int parentId)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.ParentId == parentId).ToArrayAsync();

            if (dbSupplierNotes.Length == 0)
            {
                return;
            }

            var supplierNotes = dbSupplierNotes.Select(n => Mapper.Map<SupplierNoteListItem>(n)).ToList();
            foreach (var note in supplierNotes)
            {
                children.Add(note);
                await GetChildren(children, note.Id);
            }
        }

        private async Task UpdateStatusSupplierNoteChildrenAsync(int parentId, bool deletedStatus)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.ParentId == parentId).ToArrayAsync();

            if (dbSupplierNotes.Length == 0)
            {
                return;
            }

            foreach (var note in dbSupplierNotes)
            {
                if (note.IsDeleted != deletedStatus)
                {
                    note.IsDeleted = deletedStatus;
                    note.LastUpdated = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }

                await UpdateStatusSupplierNoteChildrenAsync(note.Id, deletedStatus);
            }
        }

        private async Task DeleteSupplierNoteChildrenAsync(int parentId)
        {
            var dbSupplierNotes = await _context.SupplierNotes.Where(n => n.ParentId == parentId).ToArrayAsync();

            if (dbSupplierNotes.Length == 0)
            {
                return;
            }

            foreach (var note in dbSupplierNotes)
            {
                _context.SupplierNotes.Remove(note);
                await _context.SaveChangesAsync();

                await DeleteSupplierNoteChildrenAsync(note.Id);
            }
        }
    }
}