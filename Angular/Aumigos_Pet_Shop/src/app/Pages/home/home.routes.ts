import { Routes } from "@angular/router";
import { HomeComponent } from "./home.component";
import { VacineComponent } from "./vacine/vacine.component";
import { PetComponent } from "./pet/pet.component";

export const homeRoutes: Routes = [
    {
        path: '',
        component: HomeComponent,
        children: [
            {path: '', component: VacineComponent},
            {path: 'vacine', component: VacineComponent},
            {path: 'pet', component: PetComponent}
        ]
    },
];