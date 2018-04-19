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



		// POST api/pages/create
		[HttpPost("create")]
		public IActionResult Post([FromBody] Page page)
		{
			page.Slug = page.Title.Replace(" ", "-").ToLower();
			page.HasSidebar = page.HasSidebar ?? "no";

			var slug = _context.Pages.FirstOrDefault(it => it.Slug == page.Slug);
			if (slug != null)
			{
				return Json("pageExists");
			}
			else
			{
				_context.Pages.Add(page);
				_context.SaveChanges();

				return Json("ok");
			}
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
