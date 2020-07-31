import { Component, OnInit } from '@angular/core';
import { NCalculatorService } from '../ncalculator.service';
import { IUserInput } from '../models';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  birthGridModel: IUserInput;
  pyramidViewModel: IUserInput;
  yearModels: IUserInput;

  constructor(
    public nCal: NCalculatorService,
  ) {
    this.reset();
  }

  ngOnInit(): void {

  }

  reset(): void {
    this.birthGridModel = null;
    this.pyramidViewModel = null;
    this.yearModels = null;
  }
  onCalculate(data: any): void {
    this.reset();

    if (!data) {
      return;
    }
    const fullName: string = data.FullName;
    const nickName: string = data.NickName;
    const month: string = data.Month;
    const day: string = data.Day;
    const year: string = data.Year;

    this.updateBirthGrid(fullName, nickName, month, day, year);
    this.updatePyramidView(day, month, year);
    this.updateYearList(year, day, month);
  }

  private updateYearList(year: string, day: string, month: string): void {
    this.yearModels = {
      day: day,
      month: month,
      year: year,
    };

  }

  private updatePyramidView(day: string, month: string, year: string): void {
    this.pyramidViewModel = {
      day: day,
      month: month,
      year: year,
    };
  }

  private updateBirthGrid(fullName: string, nickName: string, month: string, day: string, year: string): void {
    this.birthGridModel = {
      day: day,
      month: month,
      name: fullName,
      nickName: nickName,
      year: year,
    };

  }
}
