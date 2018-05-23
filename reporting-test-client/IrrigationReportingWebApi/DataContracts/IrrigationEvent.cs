using System;

namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public class IrrigationEvent
	{
		public Int64 JournalId { get; set; }
		public double Bearing { get; set; }
		public int? RotationId { get; set; }
		public double? Velocity { get; set; }
		public bool? Pump { get; set; }
		public int? ScheduleId { get; set; }
		public string Substance { get; set; }
		public int PivotControllerId { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
