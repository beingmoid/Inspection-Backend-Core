using Inspection.Data.Entities;
using InspectionCore.Reposiotories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Reposiotories
{
	public class FormBuilderQuestionsRepository : EFRepository<FormBuilderQuestions, int>, IFormBuilderQuestionsRepository
    {
		public FormBuilderQuestionsRepository(RequestScope<InspectionEfContext> requestScope)
			: base(requestScope)
		{
		}
	}

	public interface IFormBuilderQuestionsRepository : IEFRepository<FormBuilderQuestions, int>
	{

	}
}
