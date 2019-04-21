import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from '../components/login/login.component';
import { DashboardComponent } from '../components/dashboard/dashboard.component';
import { AuthGuard } from '../services/auth-guard.service';
import { HomeComponent } from '../components/dashboard/home/home.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard', component: DashboardComponent, 
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        children: [
          //{ path: 'crises', component: ManageCrisesComponent },
          //{ path: 'heroes', component: ManageHeroesComponent },
          { path: '', component: HomeComponent }
        ],
      }
    ]
  },
  //{ path: 'appuntamenti', component: AppuntamentoComponent },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', component: LoginComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
