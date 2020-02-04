using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspectionCore.Reposiotories
{
	public class RequestScope
	{
		public RequestScope(IServiceProvider serviceProvider, ILogger logger, IMapper mapper, string userId, int? tenantId)
		{
			this.ServiceProvider = serviceProvider;
			this.UserId = userId;
			this.TenantId = tenantId;
			this.Mapper = mapper;
			this.Logger = logger;
		}

		public IServiceProvider ServiceProvider { get; }
		public string UserId { get; }
		public int? TenantId { get; }
		public IMapper Mapper { get; }
		public ILogger Logger { get; }
	}

	public class RequestScope<Context> : RequestScope
		where Context : InspectionEfContext
	{
		public RequestScope(IServiceProvider serviceProvider, Context context, ILogger logger, IMapper mapper, string userId, int? tenantId)
			: base(serviceProvider, logger, mapper, userId, tenantId)
		{
			this.DbContext = context;
		}

		public Context DbContext { get; }
	}
}
