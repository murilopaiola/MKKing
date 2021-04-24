import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ServerErrorComponent } from './core/server-error/server-error/server-error.component';
import { NotFoundComponent } from './core/server-error/not-found/not-found.component';

const routes: Routes = [
  { path: '', component: HomeComponent, data: {breadcrumb: 'Home'} },
  { path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule), // Enable lazy loading
        data: {breadcrumb: 'Shop'} },
  { path: 'basket', loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule), // Enable lazy loading
    data: {breadcrumb: 'Basket'} },
  { path: 'checkout', loadChildren: () => import('./checkout/checkout.module').then(mod => mod.CheckoutModule), // Enable lazy loading
    data: {breadcrumb: 'Checkout'} },
  { path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server Error'}},
  { path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'Not Found'}},
  { path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
