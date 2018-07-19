using System;
using System.Linq;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataAccess
{
	public class BearingMinuiteConverter : IBearingMinuiteConverter
	{
		double degreesPerMinute = 60.0;

		public decimal ToDegrees(int? bearingMinutes)
		{
			return bearingMinutes.HasValue == false ? 0.0m : Convert.ToDecimal(Math.Round((bearingMinutes.Value / degreesPerMinute), 6));
		}

		public int ToBearingMinutes(double bearing)
		{
			return Convert.ToInt32(bearing * degreesPerMinute);
		}
	}
}
