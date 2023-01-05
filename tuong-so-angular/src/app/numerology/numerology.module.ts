import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MainPageComponent } from './main-page/main-page.component';
import { InfoFormComponent } from './info-form/info-form.component';
import { BirthGridComponent } from './birth-grid/birth-grid.component';
import { PyramidViewComponent } from './pyramid-view/pyramid-view.component';
import { YearListComponent } from './year-list/year-list.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ClientListComponent } from './client-list/client-list.component';


@NgModule({
  declarations: [
    MainPageComponent,
    InfoFormComponent,
    BirthGridComponent,
    PyramidViewComponent,
    YearListComponent,
    ClientListComponent],
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
