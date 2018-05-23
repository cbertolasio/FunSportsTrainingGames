using System;
using System.ComponentModel.DataAnnotations;

namespace Trimble.Ag.IrrigationReporting.DataModels.Models
{
	public partial class IrrigationEvent
	{
		[Key]
		public Int64 JournalId { get; set; }
		public Int32? Bearing { get; set; }
		public Int32? RotationId { get; set; }
		public double? Velocity { get; set; }
		public string Pump { get; set; }
		public Int32? ScheduleId { get; set; }
		public string Substance { get; set; }
		public Int32 PivotControllerId { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedDayOfMonth { get; set; }
		public int CreatedDayOfWeek { get; set; }
		public int CreatedDayOfYear { get; set; }
		public int CreatedHour { get; set; }
		public int CreatedMonth { get; set; }
		public int CreatedQuarter { get; set; }
		public int CreatedWeek { get; set; }
		public int CreatedYear { get; set; }
	}
}
