import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IndexComponent } from './web/index/index.component';
import { ViewerComponent } from './game/viewer/viewer.component';
import { MasterComponent } from './game/master/master.component';
import { NotFoundComponent } from './error/not-found/not-found.component';
import { AuthGuard } from './guards/auth.guard';
import { DashboardComponent } from './web/dashboard/dashboard.component';
import { StateComponent } from './game/state/state.component';
import { VerifyComponent } from './game/verify/verify.component';
import { ProfileComponent } from './web/profile/profile.component';
import { CallbackComponent } from './core/callback/callback.component';


const routes: Routes = [
  { path: 'master/:gameId', component: MasterComponent, canActivate: [AuthGuard] },
  { path: 'verify/:initCode', component: VerifyComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'state/:gameId', component: StateComponent },
  { path: 'viewer', component: ViewerComponent },
  { path: 'index', component: IndexComponent },
  { path: 'callback', component: CallbackComponent },
  { path: '', redirectTo: '/index', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
