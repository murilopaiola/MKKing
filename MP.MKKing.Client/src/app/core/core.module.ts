import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { ServerErrorComponent } from './server-error/server-error/server-error.component';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { NotFoundComponent } from './server-error/not-found/not-found.component';


// Singleton modules
@NgModule({
  declarations: [NavBarComponent,
    ServerErrorComponent,
    SectionHeaderComponent,
    NotFoundComponent],
  imports: [
    CommonModule,
    RouterModule,
    BreadcrumbModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports: [NavBarComponent,
    SectionHeaderComponent]
})
export class CoreModule { }
