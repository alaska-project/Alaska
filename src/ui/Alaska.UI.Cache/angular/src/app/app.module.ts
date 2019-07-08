import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AlaskaUiCacheModule } from '@alaska-project/ui';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AlaskaUiCacheModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
