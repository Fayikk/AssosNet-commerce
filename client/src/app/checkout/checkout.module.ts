import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutRoutingModule } from './checkout-routing.module';



@NgModule({
  declarations: [
    CheckoutComponent
  ],
  imports: [
    CheckoutRoutingModule
  ],
  exports:[
    CheckoutRoutingModule
  ]
})
export class CheckoutModule { }
