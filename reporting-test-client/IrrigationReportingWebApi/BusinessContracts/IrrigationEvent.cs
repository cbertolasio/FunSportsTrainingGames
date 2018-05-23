using System;

namespace Trimble.Ag.IrrigationReporting.BusinessContracts
{
	public class IrrigationEvent
	{
		public Int64 JournalId { get; set; }
		public double Bearing { get; set; }
		public string Direction { get; set; }
		public double Velocity { get; set; }
		public bool? IsPumpOn { get; set; }
		public int ScheduleId { get; set; }
		public string Substance { get; set; }
		public int PivotControllerId { get; set; }
		public DateTime CreatedDate { get; set; }
		public string DisplaySubstance { get; set; }
		public double TimeBetweenEvents { get; set; }
	}
}
