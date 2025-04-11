import { Component, OnDestroy, OnInit } from '@angular/core';
import { PetTypeEnum, serviceTypeEnum } from '../../Model/enum/typeEnum.enum';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { ServiceModel } from '../../Model/ServiceModel.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent  implements OnInit, OnDestroy{
  subLIst: Subscription[] = [];
  typeService: serviceTypeEnum = serviceTypeEnum.pet;
  petDayList: ServiceModel[];
  vetDayList: ServiceModel[];
  hourList = ["8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30"];

  constructor(private http:HttpClient){}

  ngOnInit(): void {
    this.getServices(new Date().toLocaleDateString('ja-JP', {year: 'numeric', month: '2-digit', day: '2-digit'}).toString());
  }

  getServices(date: string){
    this.subLIst.push(this.http.get<ServiceModel[]>(`api/v1/vetservices/date?date=${date}`).subscribe(res => {
      this.vetDayList = res;
      this.vetDayList.map(item => item.scheduledDate = new Date(item.scheduledDate));
    }));
    this.subLIst.push(this.http.get<ServiceModel[]>(`api/v1/petservice/date?date=2025/04/06`).subscribe(res => {
      this.petDayList = res;
      this.petDayList.map(item => item.scheduledDate = new Date(item.scheduledDate));
    }));
  }

  getServiceAtHour(hour: string, service: ServiceModel[]): any | null{
    if(service){
      return service.find(service => {
        const h = service.scheduledDate.getHours().toString();
        const m = service.scheduledDate.getMinutes().toString().padStart(2, "0");
        return `${h}:${m}` === hour;
      }) || null;  
    }
    return null;
  }
  
  getTypeString(type: PetTypeEnum){
    switch(type){
      case 1:
        return "Cachorro";
      case 2:
        return "Gato";
      default:
        return "Não informado";
    }
  }

  fotmatPhone(phone:string){
    return phone.replace(/^(\d{2})(\d{5})(\d{4})$/, "($1) $2-$3");
  }

  expandSideBar(){
    const sideBar = document.querySelector('.side-menu');
    if(!sideBar?.classList.contains("expanded")){
      sideBar?.classList.add("expanded");
    }
    else{
      sideBar.classList.remove("expanded");
    }
  }

  ngOnDestroy(): void {
    this.subLIst.forEach(sub => sub.unsubscribe());
  }
}