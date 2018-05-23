using System;
using System.Data;

namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IConnectionFactory
	{
		IDbConnection Create();
	}
}
