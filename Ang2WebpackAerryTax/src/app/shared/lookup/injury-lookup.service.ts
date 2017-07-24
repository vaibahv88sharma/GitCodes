import { Injectable } from '@angular/core';

import { MOCK_INJURY_LOOKUP } from './mock-injuries-lookup';

@Injectable()
export class InjuryLookupService {

  constructor() { }

  getInjuriesLookup(): string[] {
    return MOCK_INJURY_LOOKUP;
  }

}
