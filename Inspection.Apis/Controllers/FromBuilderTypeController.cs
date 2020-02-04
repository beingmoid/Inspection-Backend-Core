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

    public class FromBuilderTypeController : BaseController<FormBuilderType, int>
    {
        public FromBuilderTypeController(RequestScope scopeContext, IFormBuilderTypeServices service) : base(scopeContext, service)
        {
        }
    }
}