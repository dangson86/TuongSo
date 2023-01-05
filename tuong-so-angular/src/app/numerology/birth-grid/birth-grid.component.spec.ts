import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BirthGridComponent } from './birth-grid.component';

describe('BirthGridComponent', () => {
  let component: BirthGridComponent;
  let fixture: ComponentFixture<BirthGridComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ BirthGridComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BirthGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
