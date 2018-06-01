using System;

namespace Trimble.Ag.IrrigationReporting.BusinessContracts
{
	public class IrrigationEventSummary
	{
		public int Count { get; set; }
		public string Direction { get; set; }
		public string DisplaySubstance { get; set; }
		public bool IsPumpOn { get; set; }
	}
}
