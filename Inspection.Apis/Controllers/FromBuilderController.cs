using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspection.Data.Entities;
using Inspection.Services;
using InspectionCore.Apis;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspection.Apis.Controllers
{

    public class FromBuilderController : BaseController<FormBuilder, int>
    {
        public FromBuilderController(RequestScope scopeContext, IFormBuilderServices service) : base(scopeContext, service)
        {
        }
    }
}