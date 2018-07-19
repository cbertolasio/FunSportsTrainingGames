using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models
{
	public class IrrigationEventsResponse
	{
		public IEnumerable<IrrigationEventSummary> Summary { get; set; }
		public IEnumerable<IrrigationEvent> Events { get; set; }
		public int TotalEvents { get; set; }
		public int TotalEventsWithUnknownBearings { get; set; }
		public double PercentageOfZeroBearings { get; set; }
		public IEnumerable<IrrigationEventBoundary> Boundaries { get; set; }

		public IrrigationEventsResponse()
		{
			Summary = new Collection<IrrigationEventSummary>();
			Events = new Collection<IrrigationEvent>();
		}
	}
}
