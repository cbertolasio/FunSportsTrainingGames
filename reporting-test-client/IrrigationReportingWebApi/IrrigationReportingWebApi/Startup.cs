using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trimble.Ag.IrrigationReporting.DependencyInjection;

namespace IrrigationReportingWebApi
{
	public class Startup
	{
		public Startup(IHostingEnvironment env, IConfiguration configuration)
		{
			var builder = new ConfigurationBuilder();

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets<Startup>();
			}
				
			builder.AddEnvironmentVariables();
			
			Configuration = builder.Build();
		}

		public IConfiguration Configuration { get; }
		public IContainer ApplicationContainer { get; private set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options => options.AddPolicy("AllowAll",
				policy => policy.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()));

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context => {
					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Instance = context.HttpContext.Request.Path,
						Status = StatusCodes.Status400BadRequest,
						Type = "https://asp.net/core",
						Detail = "Please refer to the errors property for additional details."
					};

					return new BadRequestObjectResult(problemDetails)
					{
						ContentTypes = { "application/problem+json", "application/problem+xml" }
					};
				};
			});

			var connectionString = Configuration.GetConnectionString("odbc-agdatawarehouse");
			
			//services.AddEntityFrameworkNpgsql().AddDbContext<AgDataWarehouseDbContext>(options => 
			//	options.UseNpgsql(connectionString)
			//	.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
			//);

			ApplicationContainer = WebApiDependencyConfiguration.Configure(services);
			return new AutofacServiceProvider(ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseForwardedHeaders();
			applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
		}
	}
}
