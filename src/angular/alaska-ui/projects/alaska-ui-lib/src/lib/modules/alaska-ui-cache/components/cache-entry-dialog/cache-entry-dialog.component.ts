import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'alui-cache-entry-dialog',
  templateUrl: './cache-entry-dialog.component.html',
  styleUrls: ['./cache-entry-dialog.component.css']
})
export class CacheEntryDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<CacheEntryDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ICacheEntryData) { 
    }

  ngOnInit() {
  }

  close(): void {
    this.dialogRef.close();
  }
}

export interface ICacheEntryData {
  key: string;
  expiration: string;
  expirationTime: Date;
  value: string;
}
