using System;
using System.Data;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataModels
{
	public class OdbcUnitOfWork : IUnitOfWork
	{
		private IDbTransaction transaction;
		private readonly Action<OdbcUnitOfWork> rolledBack;
		private readonly Action<OdbcUnitOfWork> committed;

		public OdbcUnitOfWork(IDbTransaction transaction, Action<OdbcUnitOfWork> rolledBack, Action<OdbcUnitOfWork> committed)
		{
			Transaction = transaction;
			this.transaction = transaction;
			this.rolledBack = rolledBack;
			this.committed = committed;
		}

		public IDbTransaction Transaction { get; private set; }

		public void Dispose()
		{
			if (transaction == null)
				return;

			transaction.Rollback();
			transaction.Dispose();
			rolledBack(this);
			transaction = null;
		}

		public void SaveChanges()
		{
			if (transaction == null)
				throw new InvalidOperationException("May not call save changes twice.");

			transaction.Commit();
			committed(this);
			transaction = null;
		}
	}
}
