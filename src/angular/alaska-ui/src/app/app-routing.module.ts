import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CacheAdminComponent } from 'projects/alaska-ui-lib/src/public-api';

const routes: Routes = [
  {
    path: 'cache',
    component: CacheAdminComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
