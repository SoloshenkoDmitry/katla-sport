using System;
using AutoMapper;
using DataAccessSupplierNote = KatlaSport.DataAccess.ProductSupplier.SupplierNote;

namespace KatlaSport.Services.SupplierNoteManagement
{
    public class NoteManagementMappingProfile : Profile
    {
        public NoteManagementMappingProfile()
        {
            CreateMap<DataAccessSupplierNote, SupplierNoteListItem>();
            CreateMap<DataAccessSupplierNote, SupplierNote>();
            CreateMap<UpdateSupplierNoteRequest, DataAccessSupplierNote>()
                .ForMember(r => r.LastUpdated, opt => opt.MapFrom(p => DateTime.UtcNow));
        }
    }
}