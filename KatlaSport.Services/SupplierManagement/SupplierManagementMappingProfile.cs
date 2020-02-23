using System;
using AutoMapper;
using DataAccessSupplier = KatlaSport.DataAccess.ProductSupplier.Supplier;

namespace KatlaSport.Services.SupplierManagement
{
    public sealed class SupplierManagementMappingProfile : Profile
    {
        public SupplierManagementMappingProfile()
        {
            CreateMap<DataAccessSupplier, SupplierListItem>();
            CreateMap<DataAccessSupplier, Supplier>();
            CreateMap<UpdateSupplierRequest, DataAccessSupplier>()
                .ForMember(r => r.LastUpdated, opt => opt.MapFrom(p => DateTime.UtcNow));
        }
    }
}