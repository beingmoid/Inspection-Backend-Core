using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspectionCore.Reposiotories
{
	public interface IRequestInfo
	{
		IConfiguration Configuration { get; }
		int? TenantId { get; }
	}
}
