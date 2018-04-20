using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ucmsapi.Data;
using ucmsapi.Models;

namespace ucmsapi.Controllers
{
	[Route("api/[controller]")]
	public class SidebarController : Controller
	{
		private readonly UCmsApiContext _context;
		public SidebarController(UCmsApiContext context)
		{
			_context = context;
		}

		// GET api/pages
		public IActionResult Get()
		{
			Sidebar sidebar = _context.Sidebar.FirstOrDefault();

			return Json(sidebar);
		}

		// GET api/edit
		[Route("edit")]
		public IActionResult Put([FromBody]Sidebar sidebar)
		{
			_context.Sidebar.Update(sidebar);
			_context.SaveChanges();

			return Json("ok");
		}
	}
}