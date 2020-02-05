using Inspection.Data.Entities;
using Inspection.Data.Entities;
using Inspection.Data.Reposiotories;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Services
{
    public class FormBuilderTypeServices : BaseService<FormBuilderType, int>, IFormBuilderTypeServices
    {
        public FormBuilderTypeServices(RequestScope requestScope, IFormBuilderTypeRepository repository) : base(requestScope,repository)
        {
        }
    }

    public interface IFormBuilderTypeServices : IBaseService<FormBuilderType,int>
    {

    }
}
