using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inspection.Data.Entities
{
	public class Tenant : BaseEntity<int>
	{
		[NotMapped]
		public override int? TenantId => base.TenantId;
		[NotMapped]
		public override string CompanyId { get => base.CompanyId; set => base.CompanyId = value; }
		public string Name { get; set; }
		public string Domain { get; set; }
		public Guid Guid { get; set; }

		private ICollection<TenantApplication> _tenantApplications;
		public ICollection<TenantApplication> TenantApplications => _tenantApplications ?? (_tenantApplications = new List<TenantApplication>());
	}
}
