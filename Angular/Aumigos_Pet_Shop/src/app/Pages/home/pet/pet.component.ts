import { Component } from '@angular/core';
import { DoubtsComponent } from '../doubts/doubts.component';

@Component({
  selector: 'app-pet',
  standalone: true,
  imports: [DoubtsComponent],
  templateUrl: './pet.component.html',
  styleUrl: './pet.component.css'
})
export class PetComponent {

}
