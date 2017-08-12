import { TestBed, inject } from '@angular/core/testing';

import { InjuryLookupService } from './injury-lookup.service';

describe('InjuryLookupService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InjuryLookupService]
    });
  });

  it('should be created', inject([InjuryLookupService], (service: InjuryLookupService) => {
    expect(service).toBeTruthy();
  }));
});
