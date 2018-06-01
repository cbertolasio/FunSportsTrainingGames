export class IrrigationEvent {
    journalId: number;
    bearing: number;
    direction: string;
    velocity: number;
    isPumpOn: false;
    scheduleId: number;
    puvitControllerId: number;
    createdDate: Date;
    displaySubstance: string;
    timeBetweenEvents: number;
}
