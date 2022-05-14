using AutoMapper;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using ReactAsp.NetCoreWebAPIApp.Model.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Core.Profiles
{
    public class PropertySalesProfile : Profile
    {
        public PropertySalesProfile()
        {
            CreateMap<Sale, PropertyModel>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => src.CreatedDateTime))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Property.Name))
                .ForMember(dest => dest.No, opt => opt.MapFrom(src => src.Property.No))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Property.Street))
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Property.City))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Property.Type))
                 .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => src.Customer.Name))
                  .ForMember(dest => dest.BuyerAddress, opt => opt.MapFrom(src => src.Customer.Address));
        }
    }
}
