import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyRegisterNestedFormComponent } from './company-register-nested-form.component';

describe('CompanyRegisterNestedFormComponent', () => {
  let component: CompanyRegisterNestedFormComponent;
  let fixture: ComponentFixture<CompanyRegisterNestedFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyRegisterNestedFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyRegisterNestedFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
