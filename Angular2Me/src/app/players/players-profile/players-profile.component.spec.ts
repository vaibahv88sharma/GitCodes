import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayersProfileComponent } from './players-profile.component';

describe('PlayersProfileComponent', () => {
  let component: PlayersProfileComponent;
  let fixture: ComponentFixture<PlayersProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayersProfileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayersProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
