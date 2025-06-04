import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/Login/login.component';
import { DashboardComponent } from './Pages/dashboard/dashboard.component';
import { ErrorComponent } from './Pages/error/error.component';
import { authGuard } from './guards/auth.guard';
import { ResetpasswordComponent } from './Pages/passwordPages/resetpassword/resetpassword.component';
import { UserComponent } from './Pages/user/user.component';
import { UpdatepasswordComponent } from './Pages/passwordPages/updatepassword/updatepassword.component';
import { ConfigurationComponent } from './Pages/configuration/configuration.component';
import { ForgotPasswordComponent } from './Pages/passwordPages/forgot-password/forgot-password.component';

export const routes: Routes = [
    {
        path: 'home',
        loadChildren: () => import('./Pages/home/home.routes').then(r => r.homeRoutes)
    },
    { path: 'login', component: LoginComponent },
    { path: 'Login', component: LoginComponent },
    { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
    { path: 'user', component: UserComponent, canActivate: [authGuard]},
    //{ path: 'updatePassword', component: UpdatepasswordComponent, canActivate: [authGuard]},
    { path: 'forgot', component: ForgotPasswordComponent },
    { path: 'configuration', component: ConfigurationComponent, canActivate: [authGuard]},
    { path: 'reset', component: ResetpasswordComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: '**', component: ErrorComponent }
];
