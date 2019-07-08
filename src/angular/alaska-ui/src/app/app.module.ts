import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AlaskaUiCacheModule } from 'projects/alaska-ui-lib/src/public-api';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AlaskaUiCacheModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
