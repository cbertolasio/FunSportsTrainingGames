using System;
using Microsoft.Extensions.Configuration;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataModels
{
	public class OdbcAgDataWarehouseConnectionStringProvider : IConnectionStringProvider
	{
		public string GetConnectionString()
		{
			ConnectionName = "odbcagdatawarehouse";
			return configuration.GetConnectionString(ConnectionName);
		}

		public string ConnectionName { get; private set; }

		public OdbcAgDataWarehouseConnectionStringProvider(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		private readonly IConfiguration configuration;
	}
}
