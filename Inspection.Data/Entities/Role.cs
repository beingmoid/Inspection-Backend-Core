using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class Role : BaseEntity<int>
	{
		public string Name { get; set; }
		public RoleType RoleType { get; set; }

		private ICollection<RoleRight> _roleRights;
		public ICollection<RoleRight> RoleRights => _roleRights ?? (_roleRights = new List<RoleRight>());

		private ICollection<User> _users;
		public ICollection<User> Users => _users ?? (_users = new List<User>());
	}
}
