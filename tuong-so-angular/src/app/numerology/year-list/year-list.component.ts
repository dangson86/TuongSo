import { Component, Input, OnInit } from '@angular/core';

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
  @Input() model: IYearModel[];

  constructor() { }

  ngOnInit(): void {
  }

}
