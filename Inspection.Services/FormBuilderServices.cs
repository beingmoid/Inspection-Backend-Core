using Inspection.Data.Entities;
using Inspection.Data.Reposiotories;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Services
{
    public class FormBuilderServices : BaseService<FormBuilder, int>, IFormBuilderServices
    {
        public FormBuilderServices(RequestScope requestScope, IFormBuilderRepository repository) : base(requestScope,repository)
        {
        }
        
    }

    public interface IFormBuilderServices : IBaseService<FormBuilder,int>
    {

    }
}
