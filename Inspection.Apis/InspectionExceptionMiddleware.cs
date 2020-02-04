using InspectionCore.Reposiotories;
using InspectionCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Inspection.Apis
{
	public class InspectionExceptionMiddleware
	{
		private readonly RequestDelegate next;

		public InspectionExceptionMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context, IHostingEnvironment env, RequestScope requestScope)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				context.Response.ContentType = "application/json";

				object[] errors = null;
				switch (ex)
				{
					case ServiceException serviceException:
						context.Response.StatusCode = (int)serviceException.HttpStatusCode;
						errors = serviceException.Errros;
						requestScope.Logger.LogWarning(new EventId(9999, "Inspection"), serviceException, string.Join(Environment.NewLine, serviceException.Errros));
						break;
					case Exception exception:
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						errors = new[] { env.IsProduction() ? (object)"Internal Server Error." : exception };
						requestScope.Logger.LogError(new EventId(9999, "Inspection"), ex, ex.Message);
						break;
				}

				await context.Response.WriteAsync(JsonConvert.SerializeObject(new
				{
					statusCode = context.Response.StatusCode,
					errors
				}));
			}
		}
	}
}
