using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class FormBuilderQuestionsResponse : BaseEntity<int>
	{
     
        public TypeResponse ResponseType { get; set; }
     
        public int MaxScore { get; set; }
        public int Scored { get; set; }
        public string ResponseText { get; set; }


        public int FormBuilderQuestionsId { get; set; }
        public FormBuilderQuestions FormBuilderQuestions { get; set; }


    }
}
