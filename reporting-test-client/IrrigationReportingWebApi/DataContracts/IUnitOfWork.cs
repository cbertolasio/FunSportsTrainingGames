using System;

namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IUnitOfWork
	{
		void Dispose();

		void SaveChanges();
	}
}
