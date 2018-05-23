using System;
using System.Linq;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataAccess
{
	public class PumpStatusConverter : IPumpStatusConverter
	{
		public bool GetPumpStatus(string pumpValue)
		{
			bool result;
			bool.TryParse(pumpValue, out result);

			return result;
		}
	}
}
