using Inspection.Data.Entities;
using InspectionCore.Reposiotories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Inspection.Data.Reposiotories
{
	public class FormBuilderRepository : EFRepository<FormBuilder, int>, IFormBuilderRepository
    {
		public FormBuilderRepository(RequestScope<InspectionEfContext> requestScope)
			: base(requestScope)
		{
		}
		protected override IQueryable<FormBuilder> Query => base.Query.Include(o=>o.FormBuilderQuestions);
	}

	public interface IFormBuilderRepository : IEFRepository<FormBuilder, int>
	{

	}
}
