using Inspection.Data.Entities;
using InspectionCore.Reposiotories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Reposiotories
{
	public class FormBuilderQuestionsResponseRepository : EFRepository<FormBuilderQuestionsResponse, int>, IFormBuilderQuestionsResponseRepository
    {
		public FormBuilderQuestionsResponseRepository(RequestScope<InspectionEfContext> requestScope)
			: base(requestScope)
		{
		}
	}

	public interface IFormBuilderQuestionsResponseRepository : IEFRepository<FormBuilderQuestionsResponse, int>
	{

	}
}
