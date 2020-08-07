import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ValidationErrors } from '@angular/forms';

@Component({
  selector: 'app-info-form',
  templateUrl: './info-form.component.html',
  styleUrls: ['./info-form.component.scss']
})
export class InfoFormComponent implements OnInit {
  readonly months = [];
  readonly days = [];
  @Output() calculate = new EventEmitter();


  formGroup: FormGroup;

  constructor(public fb: FormBuilder) {

    for (let i = 0; i < 12; i++) {
      this.months.push(`${i + 1}`);
    }
    for (let i = 0; i < 31; i++) {
      this.days.push(`${i + 1}`);
    }

    this.formGroup = this.fb.group({
      FullName: ['', Validators.required],
      NickName: [''],
      Month: ['', Validators.compose([
        Validators.required,
        Validators.min(1),
        Validators.max(12),
        Validators.pattern('[0-9]+'),
      ])],
      Day: ['', Validators.compose([
        Validators.required,
        Validators.min(1),
        Validators.max(31),
        Validators.pattern('[0-9]+'),
      ])],
      Year: ['', Validators.compose([
        Validators.required,
        Validators.min(1),
        Validators.max(9999),
        Validators.pattern('[0-9]+'),
      ])],
    });
  }

  ngOnInit(): void {
    // setTimeout(() => {
    //   this.calculate.emit(this.formGroup.value);
    // }, 1000);
  }

  onCalculate(): void {
    if (!this.formGroup.valid) {
      this.calculate.emit(null);
      Object.keys(this.formGroup.controls).forEach(key => {
        const controlErrors: ValidationErrors = this.formGroup.get(key).errors;
        console.log(key, controlErrors);
      });
      alert(`Invalid form`);
    } else {
      this.calculate.emit(this.formGroup.value);
    }
  }
}
