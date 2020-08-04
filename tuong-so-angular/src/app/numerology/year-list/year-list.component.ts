import { Component, Input, OnInit } from '@angular/core';
import { IUserInput } from '../models';
import { NCalculatorService } from '../ncalculator.service';

export interface IYearModel {
  Age: string;
  Year: string;
  SumResult: string;
  YearStatus: string;
  IsAMajorYear: boolean;
}

@Component({
  selector: 'app-year-list',
  templateUrl: './year-list.component.html',
  styleUrls: ['./year-list.component.scss']
})
export class YearListComponent implements OnInit {
  show: boolean;
  yearResults: IYearModel[];

  @Input() set model(input: IUserInput) {
    this.calculate(input);
  }

  constructor(private calService: NCalculatorService) { }

  ngOnInit(): void {
  }
  calculate(input: IUserInput, years = 100): void {
    if (!input) {
      this.show = false;
      return;
    }

    this.show = true;
    this.yearResults = [];

    const yearInput: number = Number.parseInt(input.year, 10);
    const scd = this.calService.CalScd(input.day, input.month, input.year);

    const firstMajorYear = this.calService.FirtMajorYear(input.day, input.month, input.year);
    const secondMajorYear = firstMajorYear + 9;
    const thirdMajorYear = secondMajorYear + 9;
    const forthMajorYear = thirdMajorYear + 9;

    for (let i = 0; i < years; i++) {
      const currentYear = yearInput + i;
      const yearString = currentYear.toString();
      let tempSum = this.SumStringValue(input.day) + this.SumStringValue(input.month) + this.SumStringValue(yearString);
      tempSum = this.SumStringValue(tempSum.toString());
      if (i === 0) {
        this.yearResults.push(
          {
            Age: (i).toString(),
            Year: yearString,
            SumResult: this.calService.ScdToString(scd),
            YearStatus: this.calService.GetYearStatus(tempSum),
            IsAMajorYear: i === firstMajorYear || i === secondMajorYear || i === thirdMajorYear || i === forthMajorYear,
          });
      }
      else {
        this.yearResults.push(
          {
            Age: (i).toString(),
            Year: yearString,
            SumResult: this.calService.ReducePY(tempSum).toString(),
            YearStatus: this.calService.GetYearStatus(tempSum),
            IsAMajorYear: i === firstMajorYear || i === secondMajorYear || i === thirdMajorYear || i === forthMajorYear,
          });
      }
    }

  }

  SumStringValue(input: string): number {
    return this.calService.SumStringValue(input);
  }
}
