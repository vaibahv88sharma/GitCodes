import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerProfileDetailsEntryComponent } from './player-profile-details-entry.component';

describe('PlayerProfileDetailsEntryComponent', () => {
  let component: PlayerProfileDetailsEntryComponent;
  let fixture: ComponentFixture<PlayerProfileDetailsEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerProfileDetailsEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerProfileDetailsEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
