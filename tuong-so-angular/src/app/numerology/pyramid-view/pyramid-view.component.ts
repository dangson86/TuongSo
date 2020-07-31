import { Component, Input, OnInit } from '@angular/core';
import { IUserInput } from '../models';
import { NCalculatorService } from '../ncalculator.service';

export interface IPyramidView {
  baseDay: number;
  baseMonth: number;
  baseYear: number;
  v1: number;
  v2: number;
  v3: number;
  v4: number;
  y1: number;
  y2: number;
  y3: number;
  y4: number;
}

@Component({
  selector: 'app-pyramid-view',
  templateUrl: './pyramid-view.component.html',
  styleUrls: ['./pyramid-view.component.scss']
})
export class PyramidViewComponent implements OnInit {
  baseYear: number;
  baseMonth: number;
  baseDay: number;
  sumMonthDay: number;
  sumDayYear: number;
  majorYear1: number;
  majorYear2: any;
  majorYear3: any;
  majorYear4: any;
  sumSecondLevel: number;
  sumThirdLevel: number;
  show: boolean;

  @Input() set model(input: IUserInput) {
    this.calculate(input);
  }

  constructor(private calService: NCalculatorService) { }

  ngOnInit(): void {

  }
  calculate(input: IUserInput) {
    if (!input) {
      this.show = false;
      return;
    }
    this.show = true;
    this.baseDay = this.calService.ReducePYString(input.day);
    this.baseMonth = this.calService.ReducePYString(input.month);
    this.baseYear = this.calService.ReducePYString(input.year);

    this.sumDayYear = this.calService.ReducePY(this.baseDay + this.baseYear);
    this.sumMonthDay = this.calService.ReducePY(this.baseMonth + this.baseDay);

    this.majorYear1 = this.calService.FirtMajorYear(input.day, input.month, input.year);

    this.majorYear2 = this.majorYear1 + 9;
    this.majorYear3 = this.majorYear2 + 9;
    this.majorYear4 = this.majorYear3 + 9;

    this.sumSecondLevel = this.CalSecondAndThridLevel(this.sumMonthDay ?? 0, this.sumDayYear ?? 0);
    this.sumThirdLevel = this.CalSecondAndThridLevel(this.baseMonth ?? 0, this.baseYear ?? 0);
  }
  private CalSecondAndThridLevel(left: number, right: number): number {
    let value: number;
    const acceptNumbers: number[] = [10, 11, 22];
    const total: number = left + right;
    if (acceptNumbers.indexOf(total) > -1) {
      value = total;
    }
    else {
      value = this.calService.ReducePY(total);
    }

    return value;
  }

}
