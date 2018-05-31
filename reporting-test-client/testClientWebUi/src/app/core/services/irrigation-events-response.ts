import { IrrigationEvent } from './irrigation-event';

export class IrrigationEventResponse {
    events: any[] = [];
    totalEvents: number;
    totalEventsWithUnknownBearings: number;
    percentageOfZeroBearings: number;
}
