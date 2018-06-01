using System;
using System.Linq;

namespace Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models
{
	public class IrrigationEventSummary
	{
		public int Count { get; set; }
		public string Direction { get; set; }
		public string DisplaySubstance { get; set; }
		public bool IsPumpOn { get; set; }
	}
}
