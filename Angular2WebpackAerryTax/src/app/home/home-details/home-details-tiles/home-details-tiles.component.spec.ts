import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeDetailsTilesComponent } from './home-details-tiles.component';

describe('HomeDetailsTilesComponent', () => {
  let component: HomeDetailsTilesComponent;
  let fixture: ComponentFixture<HomeDetailsTilesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeDetailsTilesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeDetailsTilesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
