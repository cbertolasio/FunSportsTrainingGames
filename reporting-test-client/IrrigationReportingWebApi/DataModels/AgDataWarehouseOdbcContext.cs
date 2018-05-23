using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataModels
{

	public class AgDataWarehouseOdbcContext : IOdbcDbContext
	{

		private readonly IDbConnection connection;
		private readonly IConnectionFactory connectionFactory;
		private readonly ReaderWriterLockSlim readWriteLock = new ReaderWriterLockSlim();
		private readonly LinkedList<OdbcUnitOfWork> unitOfWork = new LinkedList<OdbcUnitOfWork>();

		public AgDataWarehouseOdbcContext(IConnectionFactory connectionFactory)
		{
			this.connectionFactory = connectionFactory;
			connection = this.connectionFactory.Create();
		}

		public IUnitOfWork CreateUnitOfWork()
		{
			var transaction = connection.BeginTransaction();
			var uow = new OdbcUnitOfWork(transaction, RemoveTransaction, RemoveTransaction);

			readWriteLock.EnterWriteLock();
			unitOfWork.AddLast(uow);
			readWriteLock.ExitWriteLock();

			return uow;
		}

		public IDbCommand CreateCommand()
		{
			var cmd = connection.CreateCommand();

			readWriteLock.EnterReadLock();
			if (unitOfWork.Count > 0)
			{
				cmd.Transaction = unitOfWork.First.Value.Transaction;
			}

			readWriteLock.ExitReadLock();

			return cmd;
		}

		private void RemoveTransaction(OdbcUnitOfWork obj)
		{
			readWriteLock.EnterWriteLock();
			unitOfWork.Remove(obj);
			readWriteLock.ExitWriteLock();
		}

		public void Dispose()
		{
			connection.Dispose();
		}
	}
}
