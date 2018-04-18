using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ucmsapi.Data;
using ucmsapi.Models;


namespace ucmsapi.Controllers
{
	[Route("api/[controller]")]
	public class PagesController : Controller
	{
		private readonly UCmsApiContext _context;


		// GET api/pages
		public IActionResult Get()
		{
			List<Page> pages = _context.Pages.ToList();

			return Json(pages);
		}

		// GET api/pages/slug
		[HttpGet("{slug}")]
		public IActionResult Get(string slug)
		{
			Page page = _context.Pages.SingleOrDefault(it => it.Slug == slug);
			if (page == null)
			{
				return Json("PageNotFound");
			}

			return Json(page);
		}



		// POST api/<controller>
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

		public PagesController(UCmsApiContext context)
		{
			_context = context;
		}
	}
}
