import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';


// Shared modules
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    // need forRoot() to load PaginationModule with its providers
    PaginationModule.forRoot()
  ],
  exports: [PaginationModule]
})
export class SharedModule { }
