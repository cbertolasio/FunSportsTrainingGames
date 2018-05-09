import { TestBed, inject } from '@angular/core/testing';

import { InMemoryIrrigationEventsDataService } from './in-memory-irrigation-events-data.service';

describe('InMemoryIrrigationEventsDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InMemoryIrrigationEventsDataService]
    });
  });

  it('should be created', inject([InMemoryIrrigationEventsDataService], (service: InMemoryIrrigationEventsDataService) => {
    expect(service).toBeTruthy();
  }));
});
