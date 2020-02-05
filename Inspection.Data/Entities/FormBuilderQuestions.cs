using InspectionCore.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inspection.Data.Entities
{
	public class FormBuilderQuestions : BaseEntity<int>
	{
        public string Text { get; set; }
        public string Key { get; set; }//
        public TypeResponse ResponseType { get; set; }
        public bool? IsScored { get; set; }
        public int? MaxScore { get; set; }
        public int? Scored { get; set; }
        public bool IsMandatory { get; set; }
        public bool ISMultiSelect { get; set; }
        public bool IsNotified { get; set; }


        public int FormBuilderId { get; set; }
        public FormBuilder FormBuilder { get; set; }


        private ICollection<FormBuilderQuestionsResponse> _formBuilderQuestionsResponse;
        public ICollection<FormBuilderQuestionsResponse> FormBuilderQuestionsResponse => _formBuilderQuestionsResponse ?? (_formBuilderQuestionsResponse = new List<FormBuilderQuestionsResponse>());



    }
    public enum TypeResponse:int
    {
        Testbox,
        Number,
        Checkbox,
        DateTime,
        Photo,
        Annotation,
        Signature
    }
}
