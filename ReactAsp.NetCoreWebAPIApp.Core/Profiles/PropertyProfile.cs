﻿using AutoMapper;
using ReactAsp.NetCoreWebAPIApp.Data.EntityModels;
using ReactAsp.NetCoreWebAPIApp.Model.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactAsp.NetCoreWebAPIApp.Core.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<PropertyModel, Property>().ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => DateTime.Now));
                //.ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer));
        }
    }
}
