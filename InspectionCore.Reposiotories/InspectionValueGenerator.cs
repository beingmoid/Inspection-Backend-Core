using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace InspectionCore.Reposiotories
{
	public abstract class InspectionValueGenerator : ValueGenerator
	{
		public override bool GeneratesTemporaryValues => false;
	}
}
