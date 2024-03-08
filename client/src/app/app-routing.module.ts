import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContractListComponent } from './contracts/contract-list/contract-list.component';
import { ContractEditComponent } from './contracts/contract-edit/contract-edit.component';
import { AddComponent } from './add/add.component';
import { authGuard } from './_guards/auth.guard';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { ContractSearchComponent } from './contracts/contract-search/contract-search.component';
import { ContractTypeOneListComponent } from './contractType/contract-type-one-list/contract-type-one-list.component';
import { ContractTypeTwoListComponent } from './contractType/contract-type-two-list/contract-type-two-list.component';
import { ContractTypeOneAddComponent } from './contractType/contract-type-one-add/contract-type-one-add.component';
import { ContractTypeTwoAddComponent } from './contractType/contract-type-two-add/contract-type-two-add.component';
import { UsersListComponent } from './admin/users-list/users-list.component';
import { ChangePasswordComponent } from './admin/change-password/change-password.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {path: 'change-password', component: ChangePasswordComponent},
      {path: 'users', component: UsersListComponent},
      {path: 'contracts', component: ContractListComponent},
      {path: 'contract/search', component: ContractSearchComponent},
      {path: 'contract/edit/:id', component: ContractEditComponent},
      {path: 'add', component: AddComponent},
      {path: 'contractTypeOnes', component: ContractTypeOneListComponent},
      {path: 'add-contractTypeOne', component: ContractTypeOneAddComponent},
      {path: 'add-contractTypeTwo', component: ContractTypeTwoAddComponent},
      {path: 'contractTypeTwos', component: ContractTypeTwoListComponent},
    ]},
    {path: 'errors', component: TestErrorComponent},
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-error', component: ServerErrorComponent},
    {path: '**', component: NotFoundComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
