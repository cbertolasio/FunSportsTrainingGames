using System;
using System.Collections.Generic;

namespace Trimble.Ag.IrrigationReporting.BusinessContracts
{
	public interface IEventBoundaryManager
	{
		IEnumerable<IrrigationEventBoundary> GetEventBoundaries(IEnumerable<IrrigationEvent> events);
	}
}
