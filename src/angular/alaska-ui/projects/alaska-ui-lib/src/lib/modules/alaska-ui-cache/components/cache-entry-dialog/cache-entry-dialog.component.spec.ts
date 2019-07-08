import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CacheEntryDialogComponent } from './cache-entry-dialog.component';

describe('CacheEntryDialogComponent', () => {
  let component: CacheEntryDialogComponent;
  let fixture: ComponentFixture<CacheEntryDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CacheEntryDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CacheEntryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
