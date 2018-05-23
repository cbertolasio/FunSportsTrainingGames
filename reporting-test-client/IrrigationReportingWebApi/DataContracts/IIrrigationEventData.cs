using System;
using System.Collections.Generic;

namespace Trimble.Ag.IrrigationReporting.DataContracts
{
	public interface IIrrigationEventData
	{
		int CountOfEventsWithZeroBearing(IrrigationEventRequest irrigationEventRequest);
		IEnumerable<IrrigationEvent> GetEvents(IrrigationEventRequest requestData);
	}
}
