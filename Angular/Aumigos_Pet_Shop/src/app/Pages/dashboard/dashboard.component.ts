import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { serviceTypeEnum } from '../../Model/enum/serviceTypeEnum';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent  implements OnInit, OnDestroy{
  typeService: serviceTypeEnum = serviceTypeEnum.pet;

  constructor(){}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }


  populateTable(){
    switch(this.typeService){
      case serviceTypeEnum.pet:
        break;
      case serviceTypeEnum.vet:
        break;
      default:
        break;
    }
  }
}
