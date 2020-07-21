import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-info-form',
  templateUrl: './info-form.component.html',
  styleUrls: ['./info-form.component.scss']
})
export class InfoFormComponent implements OnInit {
  @Output() calculate = new EventEmitter();

  formGroup: FormGroup;

  constructor(
    public fb: FormBuilder,
  ) {
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
  }

  onCalculate(): void {
    if (!this.formGroup.valid) {
      this.calculate.emit(null);
    } else {
      this.calculate.emit(this.formGroup.value);
    }
  }
}
