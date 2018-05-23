using System;
using System.Data;
using System.Data.Odbc;
using Microsoft.Extensions.Configuration;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataModels
{
	public class OdbcConnectionFactory : IConnectionFactory
	{
		private readonly string connectionString;
		private readonly string connectionName;

		public OdbcConnectionFactory(IConnectionStringProvider connectionStringProvider)
		{
			connectionString = connectionStringProvider.GetConnectionString();
			connectionName = connectionStringProvider.ConnectionName;

			if (connectionString == null)
			{
				throw new InvalidOperationException(string.Format("Failed to find connection string named '{0}' in config.", connectionName));
			}
		}

		public IDbConnection Create()
		{
			var connection = new OdbcConnection(connectionString);
			if (connection == null)
			{
				throw new InvalidOperationException(string.Format("Failed to create a connection using the connection string named '{0}' in app/web.config.", connectionName));
			}

			connection.Open();
			return connection;
		}
	}
}
