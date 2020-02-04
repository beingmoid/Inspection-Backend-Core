using InspectionCore.Common;
using InspectionCore.Reposiotories;
using InspectionCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionCore.Apis
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public abstract class BaseController : ControllerBase
	{
		public BaseController(RequestScope scopeContext, IBaseService service)
		{

		}

		protected IBaseService Service { get; }
	}

	public abstract class BaseController<TEntity, TKey> : BaseController
		where TEntity : class, IBaseEntity<TKey>, new()
	{
		public BaseController(RequestScope scopeContext, IBaseService<TEntity, TKey> service)
			: base(scopeContext, service)
		{
			this.Service = service;
		}

		protected new IBaseService<TEntity, TKey> Service { get; }
		// GET api/values
		[HttpGet]
		public virtual async Task<ActionResult> Get()
		{
			return new JsonResult(await this.Service.Get());
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public virtual async Task<ActionResult> Get(TKey id) => new JsonResult(await this.Service.GetOne(o => o.Id.Equals(id)));

		// POST api/values
		[HttpPost]
		public virtual async Task<ActionResult> Post([FromBody] TEntity entity)
		{
			var result = await Service.Insert(new[] { entity });
			if (result.Success)
			{
				return new JsonResult(result.Entities.Single());
			}
			else
			{
				return BadRequest();
			}
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public virtual async Task<ActionResult> Put(TKey id, [FromBody] TEntity entity)
		{
			var result = await Service.Update(id, entity);
			if (result.Success)
			{
				return new JsonResult(result.Entity);
			}
			else
			{
				return BadRequest();
			}
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public virtual async Task<ActionResult> Delete(TKey id)
		{
			if (await this.Service.Delete(id))
			{
				return new JsonResult("Entity deleted succesfully");
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
