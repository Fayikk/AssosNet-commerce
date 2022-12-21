import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RouterModule, Routes } from '@angular/router';
import { AccountModule } from './account.module';


const routes:Routes = [
  {path: 'login',component:LoginComponent},
  {path:'register',component:RegisterComponent}
]


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[
      RouterModule
  ]
})
export class AccountRoutingModule { }
