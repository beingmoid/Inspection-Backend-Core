using AutoMapper;
using Inspection.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Services.Mappings
{
    public class AutoMapperMappings : Profile
    {
        public AutoMapperMappings()
        {
            this.CreateMap<User>();
        }
    }
}
