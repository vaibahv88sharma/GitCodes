import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeDetailsTileTabComponent } from './home-details-tile-tab.component';

describe('HomeDetailsTileTabComponent', () => {
  let component: HomeDetailsTileTabComponent;
  let fixture: ComponentFixture<HomeDetailsTileTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeDetailsTileTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeDetailsTileTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
