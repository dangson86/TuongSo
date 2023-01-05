import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';


@Component({
  selector: 'app-info-form',
  templateUrl: './info-form.component.html',
  styleUrls: ['./info-form.component.scss']
})
export class InfoFormComponent implements OnInit, OnDestroy {
  readonly months = [];
  readonly days = [];
  @Output() calculate = new EventEmitter();

  readonly isDestroyed = new Subject<void>()

  formGroup: FormGroup;

  constructor(public fb: FormBuilder, private router: Router, private route: ActivatedRoute) {

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

  ngOnDestroy(): void {
    this.isDestroyed.next();
    this.isDestroyed.complete();
  }

  ngOnInit(): void {
    this.route.queryParams.pipe(
    ).subscribe(e => {
      if (e?.FullName && e?.Month) {
        this.formGroup.setValue(e);
      }
    });
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
  saveInfo() {
    if (this.formGroup.valid) {
      let currentClientsString = localStorage.getItem("clients")
      let currentClients: any[] = currentClientsString == null ? [] : JSON.parse(currentClientsString);
      currentClients.push(this.formGroup.value);
      localStorage.setItem("clients", JSON.stringify(currentClients));
      alert("Saved");
    }
  }

  loadInfo() {
    this.router.navigate(["clients"]);
  }
}
