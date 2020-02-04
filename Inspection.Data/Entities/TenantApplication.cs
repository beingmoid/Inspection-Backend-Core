using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class TenantApplication : BaseEntity<int>
	{
		public int ApplicationId { get; set; }

		public Tenant Tenant { get; set; }
		public Application Application { get; set; }
	}
}
