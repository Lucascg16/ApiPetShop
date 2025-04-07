import { Component, OnDestroy, OnInit } from '@angular/core';
import { serviceTypeEnum } from '../../Model/enum/serviceTypeEnum';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { ServiceModel } from '../../Model/ServiceModel';

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
  hourList = ["8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30"]
  
  constructor(private http:HttpClient){}

  ngOnInit(): void {
    this.getServices(new Date().toLocaleDateString('ja-JP', {year: 'numeric', month: '2-digit', day: '2-digit'}).toString());
  }

  getServices(date: string){
    this.subLIst.push(this.http.get<ServiceModel[]>(`api/v1/vetservices/date?date=${date}`).subscribe(res => {
      this.vetDayList = res;
      this.vetDayList.map(item => item.scheduledDate = new Date(item.scheduledDate));
    }));
    this.subLIst.push(this.http.get<ServiceModel[]>(`api/v1/petservice/date?date=${date}`).subscribe(res => {
      this.petDayList = res;
      this.petDayList.map(item => item.scheduledDate = new Date(item.scheduledDate))
    }));
  }
  
  ngOnDestroy(): void {
    this.subLIst.forEach(sub => sub.unsubscribe());
  }
}