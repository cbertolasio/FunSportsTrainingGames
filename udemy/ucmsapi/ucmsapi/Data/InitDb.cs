using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ucmsapi.Models;

namespace ucmsapi.Data
{
    public class InitDb
    {
		public static void Init(UCmsApiContext context)
		{
			context.Database.EnsureCreated();

			if (context.Pages.Any())
			{
				return;
			}

			var pages = new Page[]
			{
				new Page { Title = "Home", Slug = "home", Content = "home content", HasSidebar = "no"},
				new Page { Title = "About", Slug = "about", Content = "about content", HasSidebar = "no"},
				new Page { Title = "Services", Slug = "services", Content = "services content", HasSidebar = "no"},
				new Page { Title = "Contact", Slug = "contact", Content = "contect content", HasSidebar = "no"}
			};

			foreach (var p in pages)
			{
				context.Pages.Add(p);
			}
			context.SaveChanges();


			var sideBar = new Sidebar
			{
				ContentName = "sidebar content"
			};
			context.Sidebar.Add(sideBar);
			context.SaveChanges();

			var user = new User
			{
				UserName = "admin",
				Password = "pass",
				IsAdmin = "yes"
			};

			context.Users.Add(user);
			context.SaveChanges();

		}
	}
}
