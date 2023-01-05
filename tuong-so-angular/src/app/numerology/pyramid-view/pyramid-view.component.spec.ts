import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PyramidViewComponent } from './pyramid-view.component';

describe('PyramidViewComponent', () => {
  let component: PyramidViewComponent;
  let fixture: ComponentFixture<PyramidViewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PyramidViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PyramidViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
