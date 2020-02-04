using Inspection.Data.Entities;
using InspectionCore.Reposiotories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Reposiotories
{
	public class UserRepository : EFRepository< User, string>, IUserRepository
	{
		public UserRepository(RequestScope<InspectionEfContext> requestScope)
			: base(requestScope)
		{
		}
	}

	public interface IUserRepository : IEFRepository<User, string>
	{

	}
}
