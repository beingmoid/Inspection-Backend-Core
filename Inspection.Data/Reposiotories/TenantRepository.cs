using Inspection.Data.Entities;
using InspectionCore.Reposiotories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Reposiotories
{
	public class TenantRepository : EFRepository<Tenant, int>, ITenantRepository
	{
		public TenantRepository(RequestScope<InspectionEfContext> requestScope)
			: base(requestScope)
		{
		}
	}

	public interface ITenantRepository : IEFRepository<Tenant, int>
	{

	}
}
