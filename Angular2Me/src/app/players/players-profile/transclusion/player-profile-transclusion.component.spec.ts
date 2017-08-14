import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerProfileTransclusionComponent } from './player-profile-transclusion.component';

describe('PlayerProfileTransclusionComponent', () => {
  let component: PlayerProfileTransclusionComponent;
  let fixture: ComponentFixture<PlayerProfileTransclusionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerProfileTransclusionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerProfileTransclusionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
