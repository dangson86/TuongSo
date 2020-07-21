import { Component, OnInit } from '@angular/core';
import { IBirthGrid } from '../birth-grid/birth-grid.component';
import { NCalculatorService } from '../ncalculator.service';
import { IPyramidView } from '../pyramid-view/pyramid-view.component';
import { IYearModel } from '../year-list/year-list.component';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent implements OnInit {

  birthGridModel: IBirthGrid;
  pyramidViewModel: IPyramidView;
  yearModels: IYearModel[];

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
    this.yearModels = [];
    const yearNum = Number(year);
    const scd = this.nCal.CalScd(day, month, year);
    const firstMajorYear = this.nCal.FirtMajorYear(day, month, year);
    const majorYears = [firstMajorYear, firstMajorYear + 9, firstMajorYear + 18, firstMajorYear + 27];
    for (let i = 0; i < 100; i++) {
      const element = 100[i];
      let currentScd = scd + i;
      if (i === 0) {
        currentScd = scd;
      } else if (currentScd % 9 === 0) {
        currentScd = 9;
      } else {
        currentScd = currentScd % 9;
      }
      this.yearModels.push({
        Age: i.toString(),
        Year: (yearNum + i).toString(),
        SumResult: (currentScd).toString(),
        YearStatus: this.nCal.GetYearStatus(scd + i),
        IsAMajorYear: majorYears.indexOf(i) >= 0,
      });
    }
  }

  private updatePyramidView(day: string, month: string, year: string): void {
    const y1Value = this.nCal.FirtMajorYear(day, month, year);
    const baseDay = this.nCal.ReducePYString(day);
    const baseMonth = this.nCal.ReducePYString(month);
    const baseYear = this.nCal.ReducePYString(year);
    const v1 = this.nCal.ReducePY(baseDay + baseMonth);
    const v2 = this.nCal.ReducePY(baseDay + baseYear);
    const v3 = this.nCal.ReducePY(v1 + v2);
    const v4 = this.nCal.ReducePY(baseYear + baseMonth);

    this.pyramidViewModel = {
      baseDay,
      baseMonth,
      baseYear,
      v1,
      v2,
      v3,
      v4,
      y1: y1Value,
      y2: y1Value + 9,
      y3: y1Value + 18,
      y4: y1Value + 27,
    };
  }

  private updateBirthGrid(fullName: string, nickName: string, month: string, day: string, year: string): void {
    this.birthGridModel = {
      date: {} as any,
      name: {} as any,
    };
    const nameNumbers = this.nCal.NameToNumbers(fullName + nickName);
    const dateNumbers = [];
    (month + day + year).split('').forEach(c => {
      const num = Number(c);
      if (num > 0) {
        dateNumbers.push(num);
      }
    });


    const nameNumberGroup = this.groupBy(nameNumbers, e => e.toString(), e => e);
    Object.keys(nameNumberGroup).forEach(key => {
      this.birthGridModel.name['n' + key] = nameNumberGroup[key];
    });
    const dateNumberGroup = this.groupBy(dateNumbers, e => e.toString(), e => e);
    Object.keys(dateNumberGroup).forEach(key => {
      this.birthGridModel.date['n' + key] = dateNumberGroup[key];
    });
  }

  private groupBy<TIn, TOut>(xs: TIn[], getKey: ((e: TIn) => string), getItem: ((e: TIn) => TOut)): { [key: string]: TOut[] } {
    return xs.reduce((rv, x) => {
      const key = getKey(x);
      const item = getItem(x);
      (rv[key] = rv[key] || []).push(item);
      return rv;
    }, {});
  }
}
