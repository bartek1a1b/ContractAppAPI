import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ContractListComponent } from './contracts/contract-list/contract-list.component';
import { ContractEditComponent } from './contracts/contract-edit/contract-edit.component';
import { AddComponent } from './add/add.component';
import { SharedModule } from './_modules/shared.module';
import { TestErrorComponent } from './errors/test-error/test-error.component';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { ContractSearchComponent } from './contracts/contract-search/contract-search.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { ContractTypeOneListComponent } from './contractType/contract-type-one-list/contract-type-one-list.component';
import { ContractTypeTwoListComponent } from './contractType/contract-type-two-list/contract-type-two-list.component';
import { ContractTypeOneAddComponent } from './contractType/contract-type-one-add/contract-type-one-add.component';
import { ContractTypeTwoAddComponent } from './contractType/contract-type-two-add/contract-type-two-add.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    ContractListComponent,
    ContractEditComponent,
    AddComponent,
    TestErrorComponent,
    NotFoundComponent,
    ServerErrorComponent,
    ContractSearchComponent,
    ContractTypeOneListComponent,
    ContractTypeTwoListComponent,
    ContractTypeOneAddComponent,
    ContractTypeTwoAddComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
