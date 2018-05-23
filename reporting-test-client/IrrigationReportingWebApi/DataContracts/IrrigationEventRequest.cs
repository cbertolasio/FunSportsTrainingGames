using System;

namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public class IrrigationEventRequest
	{
		public DateTime StartDate { get; set; }
		public DateTime StopDate { get; set; }
		public double StartBearing { get; set; }
		public double StopBearing { get; set; }
		public TimeData StartAt { get; set; }
		public TimeData StopAt { get; set; }
		public int PivotId { get; set; }
	}
}
