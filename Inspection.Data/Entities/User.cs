using InspectionCore.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Inspection.Data.Entities
{
	public class User : BaseEntity<string>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public override string Id { get => base.Id; set => base.Id = value; }
		[JsonIgnore]
		public string Password { get; set; }
		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
}
