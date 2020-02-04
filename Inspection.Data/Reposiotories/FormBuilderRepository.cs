using Inspection.Data.Entities;
using InspectionCore.Reposiotories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Reposiotories
{
	public class FormBuilderRepository : EFRepository<FormBuilder, int>, IFormBuilderRepository
    {
		public FormBuilderRepository(RequestScope<InspectionEfContext> requestScope)
			: base(requestScope)
		{
		}
	}

	public interface IFormBuilderRepository : IEFRepository<FormBuilder, int>
	{

	}
}
