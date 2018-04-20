using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ucmsapi.Data;
using ucmsapi.Models;

namespace ucmsapi.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class UsersController : Controller
	{

		// POST api/users/register
		[HttpPost("register")]
		public IActionResult Register([FromBody] User user)
		{
			var username = _context.Users.FirstOrDefault(it => it.UserName == user.UserName);
			if (username != null)
			{
				return Json("userExists");
			}
			else
			{
				_context.Users.Add(user);
				_context.SaveChanges();

				return Json("ok");
			}
		}

		// POST api/users/login
		[HttpPost("login")]
		public IActionResult Login([FromBody] User user)
		{
			var existingUser = _context.Users.FirstOrDefault(it => it.UserName == user.UserName  && it.Password == user.Password);
			if (existingUser != null)
			{
				return Json(existingUser.UserName);
			}
			else
			{
				return Json("invalidLogin");
			}
		}

		public UsersController(UCmsApiContext context)
		{
			_context = context;
		}

		private readonly UCmsApiContext _context;
	}
}