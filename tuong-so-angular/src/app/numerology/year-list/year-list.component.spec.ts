import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { YearListComponent } from './year-list.component';

describe('YearListComponent', () => {
  let component: YearListComponent;
  let fixture: ComponentFixture<YearListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ YearListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(YearListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
