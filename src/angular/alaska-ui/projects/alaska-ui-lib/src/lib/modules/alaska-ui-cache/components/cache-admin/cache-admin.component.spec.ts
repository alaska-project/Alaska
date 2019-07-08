import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CacheAdminComponent } from './cache-admin.component';

describe('CacheAdminComponent', () => {
  let component: CacheAdminComponent;
  let fixture: ComponentFixture<CacheAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CacheAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CacheAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
