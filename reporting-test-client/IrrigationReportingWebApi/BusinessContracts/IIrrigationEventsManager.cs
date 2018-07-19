using System;
using System.Collections.Generic;

namespace Trimble.Ag.IrrigationReporting.BusinessContracts
{
	public interface IIrrigationEventsManager
	{
		IEnumerable<IrrigationEventBoundary> GetEventBoundaries(IEnumerable<IrrigationEvent> testEvents);
		double CalculateZeroBearingEventPercentage(int totalEvents, int zeroBearingEvents);
		int CountOfEventsWithZeroBearing(IrrigationEventRequest requestData);
		IEnumerable<IrrigationEvent> GetEvents(IrrigationEventRequest requestData);
		IEnumerable<IrrigationEventSummary> GetEventSummary(IEnumerable<IrrigationEvent> irrigationEvents);
	}
}
