using System;
using System.Linq;

namespace ucmsapi.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string IsAdmin { get; set; }
	}
}
