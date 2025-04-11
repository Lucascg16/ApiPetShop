import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(){}

  toggleActive(classpage: string){
    const vet = document.querySelector(".vet");
    const pet = document.querySelector(".pet");
    if(classpage === "vet"){
      pet?.classList.remove("active");
      vet?.classList.add("active");
    }
    if(classpage === "pet"){
      vet?.classList.remove("active");
      pet?.classList.add("active");
    }
  }
}
