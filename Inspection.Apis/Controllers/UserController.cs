using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inspection.Data.Entities;
using Inspection.Services;
using InspectionCore.Apis;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inspection.Apis.Controllers
{

    public class UserController : BaseController<User, string>
    {
        private readonly IUserService userService;
        public UserController(RequestScope scopeContext, IUserService service) : base(scopeContext, service)
        {
            userService = service;
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] Register reg) => new JsonResult(await userService.Register(reg));
        [AllowAnonymous]
        [HttpGet("EmailIsValid")]
        public async Task<ActionResult> EmailIsValid(string emailAddress)=> new JsonResult((await this.Service.GetOne(x => x.Id == emailAddress)) == null ? false : true);
        

    }
}