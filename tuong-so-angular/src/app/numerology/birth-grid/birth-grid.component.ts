import { Component, Input, OnInit } from '@angular/core';

export interface IBirthGrid {
  name: INumberGrid;
  date: INumberGrid;
}
export interface INumberGrid {
  n1: number[];
  n2: number[];
  n3: number[];
  n4: number[];
  n5: number[];
  n6: number[];
  n7: number[];
  n8: number[];
  n9: number[];
}

@Component({
  selector: 'app-birth-grid',
  templateUrl: './birth-grid.component.html',
  styleUrls: ['./birth-grid.component.scss']
})
export class BirthGridComponent implements OnInit {

  @Input() model: IBirthGrid;

  constructor() { }

  ngOnInit(): void {

  }

}
