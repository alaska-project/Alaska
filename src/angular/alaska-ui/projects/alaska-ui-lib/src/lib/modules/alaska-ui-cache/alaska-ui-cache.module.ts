import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlaskaUiCoreModule } from '../alaska-ui-core/alaska-ui-core.module';
import { CacheAdminComponent } from './components/cache-admin/cache-admin.component';
import { CacheEntryDialogComponent } from './components/cache-entry-dialog/cache-entry-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    AlaskaUiCoreModule
  ],
  declarations: [
    CacheAdminComponent,
    CacheEntryDialogComponent
  ],
  exports: [
    CacheAdminComponent
  ],
  entryComponents: [
    CacheEntryDialogComponent
  ]
})
export class AlaskaUiCacheModule { }
