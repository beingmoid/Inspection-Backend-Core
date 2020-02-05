using Inspection.Data.Entities;
using Inspection.Data.Reposiotories;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Services
{
    public class FormBuilderQuestionsServices : BaseService<FormBuilderQuestions, int>, IFormBuilderQuestionsServices
    {
        public FormBuilderQuestionsServices(RequestScope requestScope, IFormBuilderQuestionsRepository repository) : base(requestScope,repository)
        {
        }
    }

    public interface IFormBuilderQuestionsServices : IBaseService<FormBuilderQuestions, int>
    {

    }
}
