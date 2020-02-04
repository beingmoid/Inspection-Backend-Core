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

	public class LoginController : BaseController
	{
		private readonly IAuthenticationService _loginService;
		public LoginController(RequestScope scopeContext, IAuthenticationService loginService)
			: base(scopeContext, loginService)
		{
			_loginService = loginService;
		}

		[HttpPut]
		[AllowAnonymous]
		public async Task<ActionResult> Put([FromBody] LoginInfo loginInfo)
		{
			return new JsonResult(await _loginService.Authenticate(loginInfo.Login, loginInfo.Password));
		}
	}
}