import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/utility-service.service';

interface IContellations {
  name: string;
  fromDate: string;
  toDate: string;
}

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
  readonly contellations: IContellations[] = [
    {
      name: "Bao Binh (Aquarius)",
      fromDate: "1/22",
      toDate: "2/20"
    },
    {
      name: "Song Ngu (Pisces)",
      fromDate: "2/21",
      toDate: "3/20"
    },
    {
      name: "Bach Duong (Aries)",
      fromDate: "3/21",
      toDate: "4/20"
    },
    {
      name: "Kim Nguu (Taurus)",
      fromDate: "4/21",
      toDate: "5/20"
    },
    {
      name: "Song Tu (Gemini)",
      fromDate: "5/21",
      toDate: "6/20"
    },
    {
      name: "Cu Giai (Cancer)",
      fromDate: "6/21",
      toDate: "7/21"
    },
    {
      name: "Su Tu (Leo)",
      fromDate: "7/22",
      toDate: "8/22"
    },
    {
      name: "Xu Nu (Virgo)",
      fromDate: "8/23",
      toDate: "9/22"
    },
    {
      name: "Thien Binh (Libra)",
      fromDate: "9/23",
      toDate: "10/22"
    },
    {
      name: "Thien Yet (Scorpio)",
      fromDate: "10/23",
      toDate: "11/22"
    },
    {
      name: "Nhan Ma (Sagittarius)",
      fromDate: "11/23",
      toDate: "12/22"
    },
    {
      name: "Ma Ket (Capricorn)",
      fromDate: "12/23",
      toDate: "1/21"
    }
  ];
  formGroup: FormGroup;

  constructor(public fb: FormBuilder, private router: Router, private route: ActivatedRoute, private utility: UtilityService) {
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
      constellation: [''],
      comment: ['']
    });
  }

  ngOnDestroy(): void {
    this.isDestroyed.next();
    this.isDestroyed.complete();
  }

  ngOnInit(): void {
    this.route.queryParams.pipe(
      takeUntil(this.isDestroyed)
    ).subscribe(e => {
      if (e?.FullName && e?.Month) {
        this.formGroup.patchValue(e);
      }
    });
  }

  onCalculate(): void {
    if (!this.formGroup.valid) {
      this.calculate.emit(null);
      Object.keys(this.formGroup.controls).forEach(key => {
        const controlErrors: ValidationErrors = this.formGroup.get(key).errors;
        console.log(key, controlErrors);
        if (controlErrors) {
          this.utility.alert(`Missing ${key}`);
          return;
        }
      });
    } else {
      this.calculate.emit(this.formGroup.value);
    }
  }
  saveInfo() {
    if (this.formGroup.valid) {
      let currentClientsString = localStorage.getItem("clients")
      let currentClients: any[] = currentClientsString == null ? [] : JSON.parse(currentClientsString);
      let value = this.formGroup.value;
      value.createdOn = (new Date()).toLocaleString();
      currentClients.push(value);
      localStorage.setItem("clients", JSON.stringify(currentClients));
      this.utility.alert("Saved");
    }
  }

  loadInfo() {
    this.router.navigate(["clients"]);
  }
}
