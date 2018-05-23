using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataModels
{
	public class AgDataWarehouseOdbcRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : new()
	{
		IOdbcDbContext context;

		public AgDataWarehouseOdbcRepositoryBase(IOdbcDbContext context)
		{
			this.context = context;
		}

		public IOdbcDbContext Context
		{
			get
			{
				return this.context;
			}
		}

		public IEnumerable<TEntity> GetEnumerable(IDbCommand command)
		{
			using (var record = command.ExecuteReader())
			{
				Collection<TEntity> items = new Collection<TEntity>();
				while (record.Read())
				{
					items.Add(Map(record));
				}

				return items;
			}
		}

		public IEnumerable<TEntity> ToList(IDbCommand command)
		{
			using (var record = command.ExecuteReader())
			{
				List<TEntity> items = new List<TEntity>();
				while (record.Read())
				{

					items.Add(Map(record));
				}

				return items;
			}
		}

		public TEntity Map(IDataRecord record)
		{
			var objT = Activator.CreateInstance<TEntity>();
			foreach (var property in typeof(TEntity).GetProperties())
			{
				var index = record.GetOrdinal(property.Name);
				if (!record.IsDBNull(index))
				{
					property.SetValue(objT, record[index]);
				}
			}

			return objT;
		}

		public int ExecuteScalar(IDbCommand cmd)
		{
			var result = cmd.ExecuteScalar();
			return Convert.ToInt32(result);
		}
	}


}
