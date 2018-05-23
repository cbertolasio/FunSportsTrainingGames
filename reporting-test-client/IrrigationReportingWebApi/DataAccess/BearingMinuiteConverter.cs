using System;
using System.Linq;
using Trimble.Ag.IrrigationReporting.DataContracts;

namespace Trimble.Ag.IrrigationReporting.DataAccess
{
	public class BearingMinuiteConverter : IBearingMinuiteConverter
	{
		double degreesPerMinute = 60.0;

		public double ToDegrees(int? bearingMinutes)
		{
			return bearingMinutes.HasValue == false ? 0 : Math.Round((bearingMinutes.Value / degreesPerMinute), 2);
		}

		public int ToBearingMinutes(double bearing)
		{
			return Convert.ToInt32(bearing * degreesPerMinute);
		}
	}
}
