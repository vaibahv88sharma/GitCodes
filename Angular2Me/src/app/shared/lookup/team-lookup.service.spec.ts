import { TestBed, inject } from '@angular/core/testing';

import { TeamLookupService } from './team-lookup.service';

describe('TeamLookupService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TeamLookupService]
    });
  });

  it('should be created', inject([TeamLookupService], (service: TeamLookupService) => {
    expect(service).toBeTruthy();
  }));
});
