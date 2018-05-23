using System;
using System.Collections.Generic;
using System.Data;

namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IRepositoryBase<TEntity>
	{
		TEntity Map(IDataRecord record);
		IEnumerable<TEntity> ToList(IDbCommand command);
		IOdbcDbContext Context { get; }
		IEnumerable<TEntity> GetEnumerable(IDbCommand command);
		int ExecuteScalar(IDbCommand cmd);
	}
}
