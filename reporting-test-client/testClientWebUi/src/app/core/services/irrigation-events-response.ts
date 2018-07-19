import { IrrigationEvent } from './irrigation-event';
import { IrrigationEventSummary } from './irrigation-event-summary';
import { IrrigationEventBoundary } from './irrigation-event-boundary';

export class IrrigationEventResponse {
    summary: IrrigationEventSummary[] = [];
    events: any[] = [];
    boundaries: IrrigationEventBoundary[] = [];
    totalEvents: number;
    totalEventsWithUnknownBearings: number;
    percentageOfZeroBearings: number;
}
