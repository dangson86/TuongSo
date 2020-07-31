import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NCalculatorService {


  numberToLetter: { [key: number]: string[] } = {};
  letterToNumber: { [key: string]: number } = {};

  constructor(
  ) {
    this.numberToLetter[1] = ['a', 'j', 's'];
    this.numberToLetter[2] = ['b', 'k', 't'];
    this.numberToLetter[3] = ['c', 'l', 'u'];
    this.numberToLetter[4] = ['d', 'm', 'v'];
    this.numberToLetter[5] = ['e', 'n', 'w'];
    this.numberToLetter[6] = ['f', 'o', 'x'];
    this.numberToLetter[7] = ['g', 'p', 'y'];
    this.numberToLetter[8] = ['h', 'q', 'z'];
    this.numberToLetter[9] = ['i', 'r'];

    Object.keys(this.numberToLetter).forEach(numString => {
      const num = Number(numString);
      const letterList = this.numberToLetter[num];
      letterList.forEach(letter => {
        this.letterToNumber[letter] = num;
      });
    });
  }

  NameToNumbers(inputName: string): number[] {
    const numbers: number[] = [];
    if (!!inputName) {
      inputName.split('').forEach(c => {
        Object.keys(this.letterToNumber).forEach(letter => {
          if (c.toLowerCase() === letter) {
            const numKey = this.letterToNumber[letter];
            numbers.push(numKey);
          }
        });
      });
    }

    return numbers;
  }
  ReducePY(num: number): number {
    return this.ReducePYString(num.toString());
  }
  ReducePYString(numString: string): number {
    let value = -1;
    if (!!numString && numString.length > 1) {
      return this.ReducePY(this.SumStringValue(numString));
    }
    const num = Number(numString);
    if (!Number.isNaN(num)) {
      value = num;
    }

    return value;
  }

  SumStringValue(input: string): number {
    let value = 0;
    if (!!input) {
      input.split('').forEach(c => {
        const inputNumer = Number(c);
        if (!Number.isNaN(inputNumer)) {
          value += inputNumer;
        }
      });

    }

    return value;
  }
  CalScd(day: string, month: string, year: string): number {
    const acceptNumbers = [2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 22];

    const daySum = this.SumStringValue(day);
    const monthSum = this.SumStringValue(month);
    const yearSum = this.SumStringValue(year);
    let total = daySum + monthSum + yearSum;

    if (acceptNumbers.indexOf(total) < 0) {
      total = this.SumStringValue(total.toString());
    }

    return total;
  }

  FirtMajorYear(day: string, month: string, year: string): number {
    const scd = this.CalScd(day, month, year);
    let firstMajorYear = 0;
    if (scd === 22) {
      firstMajorYear = 36 - 4;
    }
    else {
      firstMajorYear = 36 - scd;
    }
    return firstMajorYear;
  }
  GetYearStatus(py: number): string {
    if (py === 9 || py === 1) {
      return 'good';
    }
    else if (py === 4 || py === 7) {
      return 'bad';
    }
    else {
      return 'average';
    }
  }
}
