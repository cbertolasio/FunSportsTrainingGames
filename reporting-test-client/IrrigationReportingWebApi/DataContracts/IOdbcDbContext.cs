using System;
using System.Data;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IOdbcDbContext
	{
		IDbCommand CreateCommand();
		IUnitOfWork CreateUnitOfWork();
		void Dispose();
	}
}
