import { IrrigationEvent } from './irrigation-event';
import { IrrigationEventSummary } from './irrigation-event-summary';

export class IrrigationEventResponse {
    summary: IrrigationEventSummary[] = [];
    events: any[] = [];
    totalEvents: number;
    totalEventsWithUnknownBearings: number;
    percentageOfZeroBearings: number;
}
