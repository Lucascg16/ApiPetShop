import { Routes } from '@angular/router';
import { LoginComponent } from './Pages/Login/login.component';
import { HomeComponent } from './Pages/home/home.component';
import { InnerPageComponent } from './Pages/inner-page/inner-page.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    {path: '', component: HomeComponent },
    {path: 'home', component: HomeComponent},
    {path: 'login', component: LoginComponent},
    {path: 'inner', component: InnerPageComponent, canActivate: [authGuard]},
];
