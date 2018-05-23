using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Trimble.Ag.IrrigationReporting.BusinessContracts;
using Trimble.Ag.IrrigationReporting.BusinessLogic;
using Trimble.Ag.IrrigationReporting.DataAccess;
using Trimble.Ag.IrrigationReporting.DataContracts;
using Trimble.Ag.IrrigationReporting.DataModels;
using DM = Trimble.Ag.IrrigationReporting.DataModels.Models;

namespace Trimble.Ag.IrrigationReporting.DependencyInjection
{
	/// <summary>
	/// Use this DI Configuration when running form MVC / Web API context
	/// </summary>
	/// <remarks>
	/// We may want to use different lifetimes from .exe or other application contexts
	/// </remarks>
	public class WebApiDependencyConfiguration
	{
		public static IContainer Configure(IServiceCollection services)
		{
			var builder = new ContainerBuilder();

			// we use instance per lifetime scope instead of instance per request
			// see: autofac documentation => http://autofac.readthedocs.io/en/latest/integration/aspnetcore.html#differences-from-asp-net-classic
			builder.RegisterType<IrrigationEventsManager>().As<IIrrigationEventsManager>().InstancePerLifetimeScope();


			
			builder.RegisterType<OdbcAgDataWarehouseConnectionStringProvider>().As<IConnectionStringProvider>().AsSelf();

			builder.RegisterType<OdbcConnectionFactory>()
				.As<IConnectionFactory>()
				.Keyed<IConnectionFactory>("odbc-agdatawarehouse").WithParameter(
					new ResolvedParameter(
						(pi, ctx) => pi.ParameterType == typeof(IConnectionStringProvider), 
						(pi, ctx) => ctx.Resolve<OdbcAgDataWarehouseConnectionStringProvider>()))
				.InstancePerLifetimeScope();

			builder.RegisterType<AgDataWarehouseOdbcContext>().WithParameter(
				new ResolvedParameter(
					(pi, ctx) => pi.ParameterType == typeof(IConnectionFactory),
					(pi, ctx) => ctx.ResolveKeyed<IConnectionFactory>("odbc-agdatawarehouse")))
				.As<IOdbcDbContext>().Keyed<IOdbcDbContext>("odbc-agdatawarehouse");


			builder.RegisterType<AgDataWarehouseOdbcRepositoryBase<DM.IrrigationEvent>>()
				.As<IRepositoryBase<DM.IrrigationEvent>>()
				.Keyed<IRepositoryBase<DM.IrrigationEvent>>("odbc-agdatawarehouse");

			builder.RegisterType<IrrigationEventsData>().As<IIrrigationEventData>().InstancePerLifetimeScope();


			builder.RegisterType<PumpStatusConverter>().As<IPumpStatusConverter>().InstancePerLifetimeScope();
			builder.RegisterType<BearingMinuiteConverter>().As<IBearingMinuiteConverter>().InstancePerLifetimeScope();

			builder.Populate(services);
			return builder.Build();
		}
	}
}
