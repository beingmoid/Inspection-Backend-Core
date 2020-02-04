using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class Application : StaticEntity
	{
		public string Name { get; set; }

		private ICollection<TenantApplication> _tenantApplications;
		public ICollection<TenantApplication> TenantApplications => _tenantApplications ?? (_tenantApplications = new List<TenantApplication>());
	}
}
