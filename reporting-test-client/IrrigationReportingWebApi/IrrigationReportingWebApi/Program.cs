using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IrrigationReportingWebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = CreateWebHostBuilder(args);
			var host = builder.Build();

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
