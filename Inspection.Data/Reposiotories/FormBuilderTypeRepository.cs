using Inspection.Data.Entities;
using InspectionCore.Reposiotories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Reposiotories
{
	public class FormBuilderTypeRepository : EFRepository<FormBuilderType, int>, IFormBuilderTypeRepository
    {
		public FormBuilderTypeRepository(RequestScope<InspectionEfContext> requestScope)
			: base(requestScope)
		{
		}
	}

	public interface IFormBuilderTypeRepository : IEFRepository<FormBuilderType, int>
	{

	}
}
