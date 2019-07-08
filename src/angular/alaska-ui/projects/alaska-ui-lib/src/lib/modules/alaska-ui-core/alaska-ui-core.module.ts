import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AlaskaUiMaterialModule } from '../alaska-ui-material/alaska-ui-material.module';
import { ErrorDialogComponent } from './components/dialogs/error-dialog/error-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    AlaskaUiMaterialModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    AlaskaUiMaterialModule
  ],
  declarations: [
    ErrorDialogComponent
  ],
  entryComponents: [
    ErrorDialogComponent
  ]
})
export class AlaskaUiCoreModule { }
