using System;
using System.Linq;

namespace Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models
{
	public class IrrigationEventRequest
	{
		public DateTime StartDate { get; set; }
		public DateTime StopDate { get; set; }
		public double StartBearing { get; set; }
		public double StopBearing { get; set; }
		public TimeData StartAt { get; set; }
		public TimeData StopAt { get; set; }
	}
}
