import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CmpAComponent } from './cmp-a.component';

describe('CmpAComponent', () => {
  let component: CmpAComponent;
  let fixture: ComponentFixture<CmpAComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CmpAComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CmpAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
