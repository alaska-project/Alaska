import { Injectable } from '@angular/core';
import { IErrorDialogContent, ErrorDialogComponent } from '../components/dialogs/error-dialog/error-dialog.component';
import { MatDialog } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(public dialog: MatDialog) { }

  showErrorDialog(error: IErrorDialogContent) {
    this.dialog.open(ErrorDialogComponent, {
      width: '450px',
      data: error
    });
  }
}
