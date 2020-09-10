import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';


// Singleton modules
@NgModule({
  declarations: [NavBarComponent],
  imports: [
    CommonModule
  ],
  exports: [NavBarComponent]
})
export class CoreModule { }
