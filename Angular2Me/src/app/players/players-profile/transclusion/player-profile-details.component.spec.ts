import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerProfileDetailsComponent } from './player-profile-details.component';

describe('PlayerProfileDetailsComponent', () => {
  let component: PlayerProfileDetailsComponent;
  let fixture: ComponentFixture<PlayerProfileDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerProfileDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerProfileDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
