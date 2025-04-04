import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/Login/login.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { ErrorComponent } from './Pages/error/error.component';
import { authGuard } from './guards/auth.guard';
import { ForgotPasswordComponent } from './Pages/passwordPages/forgot-password/forgot-password.component';
import { ResetpasswordComponent } from './Pages/passwordPages/resetpassword/resetpassword.component';

export const routes: Routes = [
    {
        path: 'home',
        loadChildren: () => import('./Pages/home/home.routes').then(r => r.homeRoutes)
    },
    { path: 'login', component: LoginComponent },
    { path: 'dashboard', component: DashboardComponent },//canActivate: [authGuard]
    { path: 'forgot', component: ForgotPasswordComponent },
    { path: 'reset', component: ResetpasswordComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: '**', component: ErrorComponent }
];
