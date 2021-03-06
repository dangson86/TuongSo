import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MainPageComponent } from './main-page/main-page.component';
import { InfoFormComponent } from './info-form/info-form.component';
import { BirthGridComponent } from './birth-grid/birth-grid.component';
import { PyramidViewComponent } from './pyramid-view/pyramid-view.component';
import { YearListComponent } from './year-list/year-list.component';
import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [MainPageComponent, InfoFormComponent, BirthGridComponent, PyramidViewComponent, YearListComponent],
  imports: [
    CommonModule,

    ReactiveFormsModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
  ]
})
export class NumerologyModule { }
