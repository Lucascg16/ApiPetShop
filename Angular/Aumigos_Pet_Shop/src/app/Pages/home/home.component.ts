import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Handlers } from '../../Shared/Handlers';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor() { }

  toggleActive(event: MouseEvent) {
    Handlers.selectActiveHandler(event);
  }
}
