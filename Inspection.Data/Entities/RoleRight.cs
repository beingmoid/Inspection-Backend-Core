using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class RoleRight : BaseEntity<int>
	{
		public int RoleId { get; set; }
		public string ControllerId { get; set; }
		public string ControllerRightId { get; set; }

		public Role Role { get; set; }
	}
}
