using System;
using System.Collections.Generic;
using System.Linq;

namespace Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models
{
	public class IrrigationEventsResponse
	{
		public IEnumerable<IrrigationEvent> Events { get; set; }
		public int TotalEvents { get; set; }
		public int TotalEventsWithUnknownBearings { get; set; }
		public double PercentageOfZeroBearings { get; set; }
	}
}
