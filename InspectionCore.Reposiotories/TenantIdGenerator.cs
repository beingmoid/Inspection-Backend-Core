using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspectionCore.Reposiotories
{
	public class TenantIdGenerator : InspectionValueGenerator
	{
		private static bool? _isScopeAvailable;

		protected override object NextValue(EntityEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException(nameof(entry));
			}

			// Temporary fix, as migration can not resolve it.
			RequestScope scope = null;
			if (!_isScopeAvailable.HasValue)
			{
				try
				{
					scope = entry.Context.GetService<RequestScope>();
					_isScopeAvailable = true;
				}
				catch
				{
					_isScopeAvailable = false;
				}
			}
			else if (_isScopeAvailable.Value)
			{
				scope = entry.Context.GetService<RequestScope>();
			}

			return scope?.TenantId;
		}
	}
}
