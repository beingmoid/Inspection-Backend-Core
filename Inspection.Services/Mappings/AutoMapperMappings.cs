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
            this.CreateMap<FormBuilderType>();
            this.CreateMap<FormBuilder>().ForMember(x=>x.FormBuilderQuestions,o=>o.MapFrom(s=>s.FormBuilderQuestions));
            this.CreateMap<FormBuilderQuestions>();
            this.CreateMap<FormBuilderQuestionsResponse>();
        }
    }
}
