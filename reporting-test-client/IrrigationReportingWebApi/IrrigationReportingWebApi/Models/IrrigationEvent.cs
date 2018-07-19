using System;
using System.Linq;

namespace Trimble.Ag.IrrigationReporting.IrrigationReportingWebApi.Models
{
	public class IrrigationEvent
	{
		public long JournalId { get; set; }
		public decimal Bearing { get; set; }
		public string Direction { get; set; }
		public double Velocity { get; set; }
		public bool? IsPumpOn { get; set; }
		public int ScheduleId { get; set; }
		public string Substance { get; set; }
		public int PivotControllerId { get; set; }
		public DateTime CreatedDate { get; set; }
		public string DisplaySubstance { get; set; }
		public double TimeBetweenEvents { get; internal set; }
	}
}
