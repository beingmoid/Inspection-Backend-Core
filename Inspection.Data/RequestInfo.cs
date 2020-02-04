using InspectionCore.Reposiotories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data
{
	public class RequestInfo : IRequestInfo
	{
		public RequestInfo(IConfiguration configuration, int? tenantId)
		{
			this.Configuration = configuration;
			this.TenantId = tenantId;
		}

		public IConfiguration Configuration { get; }

		public int? TenantId { get; }
	}
}
