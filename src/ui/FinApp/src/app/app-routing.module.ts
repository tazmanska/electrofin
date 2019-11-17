import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountsListComponent } from './accounts/accounts-list/accounts-list.component';
import { TransactionsListComponent } from './transactions/transactions-list/transactions-list.component';

const routes: Routes = [
  { path: '', component: AccountsListComponent, pathMatch: 'full' },
  { path: 'transactions', component: TransactionsListComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
