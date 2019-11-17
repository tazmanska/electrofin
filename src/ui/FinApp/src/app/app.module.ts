import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountsListComponent } from './accounts/accounts-list/accounts-list.component';
import { MenuComponent } from './main/menu/menu.component';
import { FinAppMaterialModule } from './material-module';
import { TransactionsListComponent } from './transactions/transactions-list/transactions-list.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountsListComponent,
    MenuComponent,
    TransactionsListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FinAppMaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
