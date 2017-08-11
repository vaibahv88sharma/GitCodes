import { TestBed, inject } from '@angular/core/testing';

import { FormFieldsService } from './form-fields.service';

describe('FormFieldsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FormFieldsService]
    });
  });

  it('should be created', inject([FormFieldsService], (service: FormFieldsService) => {
    expect(service).toBeTruthy();
  }));
});
