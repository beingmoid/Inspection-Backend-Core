using Inspection.Data.Entities;
using Inspection.Data.Reposiotories;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Services
{
    public class FormBuilderQuestionsResponseServices : BaseService<FormBuilderQuestionsResponse, int>, IFormBuilderQuestionsResponseServices
    {
        public FormBuilderQuestionsResponseServices(RequestScope requestScope, IFormBuilderQuestionsResponseRepository repository) : base(requestScope,repository)
        {
        }
    }

    public interface IFormBuilderQuestionsResponseServices : IBaseService<FormBuilderQuestionsResponse, int>
    {

    }
}
