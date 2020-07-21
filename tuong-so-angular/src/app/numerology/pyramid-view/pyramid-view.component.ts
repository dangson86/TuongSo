import { Component, Input, OnInit } from '@angular/core';

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

  @Input() model: IPyramidView;

  constructor() { }

  ngOnInit(): void {

  }

}
