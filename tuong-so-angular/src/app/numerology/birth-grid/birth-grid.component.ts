import { Component, Input, OnInit } from '@angular/core';
import { IUserInput, INumberGrid } from '../models';
import { NCalculatorService } from '../ncalculator.service';

interface IGridSlot {
  nameNumber: string;
  birthNumber: string;
}

@Component({
  selector: 'app-birth-grid',
  templateUrl: './birth-grid.component.html',
  styleUrls: ['./birth-grid.component.scss']
})
export class BirthGridComponent implements OnInit {
  show: boolean;


  @Input() set model(input: IUserInput) {
    this.calculate(input);
  }

  slot1: IGridSlot;
  slot2: IGridSlot;
  slot3: IGridSlot;
  slot4: IGridSlot;
  slot5: IGridSlot;
  slot6: IGridSlot;
  slot7: IGridSlot;
  slot8: IGridSlot;
  slot9: IGridSlot;

  constructor(private calService: NCalculatorService) { }

  ngOnInit(): void {

  }
  calculate(input: IUserInput): void {
    if (!input) {
      this.show = false;
      return;
    }

    this.show = true;

    let nameToNumber = this.calService.NameToNumbers(input.name);
    let nameNumber = this.OrderIntoGroup(...nameToNumber);
    let nickNameToNumber = this.calService.NameToNumbers(input.nickName);
    let nickNameNumber = this.OrderIntoGroup(...nickNameToNumber);
    let joinNameNumber = this.ReduceNumberSlotList(nameNumber, nickNameNumber);


    let groups = this.OrderStringIntoGroup(input.day, input.month, input.year);
    let birthNumber = this.ReduceNumberSlotList(...groups);


    this.slot1 = {
      nameNumber: joinNameNumber.n1.join(','),
      birthNumber: birthNumber.n1.join(',')
    };
    this.slot2 = {
      nameNumber: joinNameNumber.n2.join(','),
      birthNumber: birthNumber.n2.join(',')
    };
    this.slot3 = {
      nameNumber: joinNameNumber.n3.join(','),
      birthNumber: birthNumber.n3.join(',')
    };
    this.slot4 = {
      nameNumber: joinNameNumber.n4.join(','),
      birthNumber: birthNumber.n4.join(',')
    };
    this.slot5 = {
      nameNumber: joinNameNumber.n5.join(','),
      birthNumber: birthNumber.n5.join(',')
    };
    this.slot6 = {
      nameNumber: joinNameNumber.n6.join(','),
      birthNumber: birthNumber.n6.join(',')
    };
    this.slot7 = {
      nameNumber: joinNameNumber.n7.join(','),
      birthNumber: birthNumber.n7.join(',')
    };
    this.slot8 = {
      nameNumber: joinNameNumber.n8.join(','),
      birthNumber: birthNumber.n8.join(',')
    };
    this.slot9 = {
      nameNumber: joinNameNumber.n9.join(','),
      birthNumber: birthNumber.n9.join(',')
    };
  }
  private OrderIntoGroup(...inputs: number[]): INumberGrid {

    const temp = this.createINumberGrid();
    inputs.forEach(input => {
      switch (input) {
        case 1:
          temp.n1.push(input);
          break;
        case 2:
          temp.n2.push(input);
          break;
        case 3:
          temp.n3.push(input);
          break;
        case 4:
          temp.n4.push(input);
          break;
        case 5:
          temp.n5.push(input);
          break;
        case 6:
          temp.n6.push(input);
          break;
        case 7:
          temp.n7.push(input);
          break;
        case 8:
          temp.n8.push(input);
          break;
        case 9:
          temp.n9.push(input);
          break;
        default:
          break;
      }
    });

    return temp;
  }
  private OrderStringIntoGroup(...inputs: string[]): INumberGrid[] {
    const list: INumberGrid[] = [];
    inputs.forEach(input => {
      if (input) {
        for (let i = 0; i < input.length; i++) {
          const c = input[i];
          try {
            const tempNum = Number.parseInt(c, 10);
            list.push(this.OrderIntoGroup(tempNum));
          } catch (error) {
          }
        }

      }
    });

    return list;
  }
  private ReduceNumberSlotList(...inputs: INumberGrid[]): INumberGrid {
    return inputs.reduce((acc, current) => {
      acc.n1 = [...acc.n1, ...current.n1];
      acc.n2 = [...acc.n2, ...current.n2];
      acc.n3 = [...acc.n3, ...current.n3];
      acc.n4 = [...acc.n4, ...current.n4];
      acc.n5 = [...acc.n5, ...current.n5];
      acc.n6 = [...acc.n6, ...current.n6];
      acc.n7 = [...acc.n7, ...current.n7];
      acc.n8 = [...acc.n8, ...current.n8];
      acc.n9 = [...acc.n9, ...current.n9];

      return acc;
    }, this.createINumberGrid());
  }
  private createINumberGrid(): INumberGrid {
    return {
      n1: [],
      n2: [],
      n3: [],
      n4: [],
      n5: [],
      n6: [],
      n7: [],
      n8: [],
      n9: []
    };
  }
}
