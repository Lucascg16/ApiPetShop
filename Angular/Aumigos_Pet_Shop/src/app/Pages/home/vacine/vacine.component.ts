import { Component } from '@angular/core';
import { DoubtsComponent } from '../doubts/doubts.component';

@Component({
  selector: 'app-vacine',
  standalone: true,
  imports: [DoubtsComponent],
  templateUrl: './vacine.component.html',
  styleUrl: './vacine.component.css'
})
export class VacineComponent {
  constructor(){}
}
