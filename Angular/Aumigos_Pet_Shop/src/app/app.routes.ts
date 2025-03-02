import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/Login/login.component';

export const routes: Routes = [
    {
        path: 'home',
        loadChildren : () => import('./Pages/home/home.routes').then(r => r.homeRoutes)
    },
    { path: 'login', component: LoginComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full'},

];
