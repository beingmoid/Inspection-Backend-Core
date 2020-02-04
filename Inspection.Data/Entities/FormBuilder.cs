using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class FormBuilder : BaseEntity<int>
	{
        public string Name { get; set; }
        public int TypeId { get; set; }
        public FormBuilderType FormBuilderType { get; set; }


    }
}
