using AutoMapper;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using ReactAsp.NetCoreWebAPIApp.Model.SalesViewModel;
using System;

namespace ReactAsp.NetCoreWebAPIApp.Core.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SalesModel, Sale>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
