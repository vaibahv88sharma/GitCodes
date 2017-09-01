import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactSubmitComponent } from './contact-submit.component';

describe('ContactSubmitComponent', () => {
  let component: ContactSubmitComponent;
  let fixture: ComponentFixture<ContactSubmitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactSubmitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactSubmitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
