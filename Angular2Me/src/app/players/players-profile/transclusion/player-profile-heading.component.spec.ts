import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerProfileHeadingComponent } from './player-profile-heading.component';

describe('PlayerProfileHeadingComponent', () => {
  let component: PlayerProfileHeadingComponent;
  let fixture: ComponentFixture<PlayerProfileHeadingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerProfileHeadingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerProfileHeadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
