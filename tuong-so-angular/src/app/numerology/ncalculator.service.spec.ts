import { TestBed } from '@angular/core/testing';

import { NCalculatorService } from './ncalculator.service';

describe('NCalculatorService', () => {
  let service: NCalculatorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NCalculatorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
