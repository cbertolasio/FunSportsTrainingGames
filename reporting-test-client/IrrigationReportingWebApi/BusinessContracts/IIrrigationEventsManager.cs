using System;
using System.Collections.Generic;

namespace Trimble.Ag.IrrigationReporting.BusinessContracts
{
	public interface IIrrigationEventsManager
	{
		double CalculateZeroBearingEventPercentage(int totalEvents, int zeroBearingEvents);
		int CountOfEventsWithZeroBearing(IrrigationEventRequest requestData);
		IEnumerable<IrrigationEvent> GetEvents(IrrigationEventRequest requestData);
	}
}
