using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class FormBuilderType : BaseEntity<int>
	{
        public string Name { get; set; }
        private ICollection<FormBuilder> _formBuilder;
        public ICollection<FormBuilder> FormBuilder => _formBuilder ?? (_formBuilder = new List<FormBuilder>());


    }
}
