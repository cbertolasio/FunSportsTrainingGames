using System;
using System.Linq;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataAccess
{
	public class PumpStatusConverter : IPumpStatusConverter
	{
		public bool GetPumpStatus(string pumpValue)
		{
			if (pumpValue == null)
			{
				return false;
			}

			var value = pumpValue.Trim().ToUpper();
			switch (value)
			{
				case "YES":
				case "Y":
				case "1":
				case "TRUE":
					return true;
				default:
					return false;
			}
		}
	}
}
