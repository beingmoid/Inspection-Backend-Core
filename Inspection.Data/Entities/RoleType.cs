using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public enum RoleType : int
	{
		Admin = 1,
		Tenant,
		Customer,
		Vendor
	}
}
