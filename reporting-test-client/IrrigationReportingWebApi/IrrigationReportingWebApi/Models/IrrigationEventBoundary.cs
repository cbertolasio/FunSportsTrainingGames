using System;
using System.Linq;

namespace Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models
{
	public class IrrigationEventBoundary
	{
		public decimal StartBearing { get; set; }
		public decimal StopBearing { get; set; }
		public decimal DegreesOfTravel { get; set; }
		public long StartJournalId { get; set; }
		public long StopJournalId { get; set; }
		public int ScheduleId { get; set; }
		public double Velocity { get; set; }
		public string Substance { get; set; }
		public int PivotControllerId { get; set; }
		public TimeSpan ElapsedTime { get; set; }
		public string Direction { get; set; }
		public decimal LastKnownBearing { get; set; }
	}
}
