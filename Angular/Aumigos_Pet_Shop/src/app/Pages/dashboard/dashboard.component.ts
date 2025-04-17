import { Component, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, of, Subscription, tap } from 'rxjs';
import { ServiceModel } from '../../Model/ServiceModel.model';
import { Helper } from '../../Shared/helper';
import { PetformComponent } from './petform/petform.component';
import { PetTypeEnum, serviceTypeEnum } from '../../Model/enum/shopEnum.enum';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  subLIst: Subscription[] = [];
  petDayList: ServiceModel[];
  vetDayList: ServiceModel[];
  hourList = ["8:00", "8:30", "9:00", "9:30", "10:00", "10:30", "11:00", "11:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30"];
  tableDate: string = new Date().toLocaleDateString('ja-JP', { year: 'numeric', month: '2-digit', day: '2-digit' }).toString();
  bsModalRef?: BsModalRef;
  date: string;

  typeService: serviceTypeEnum = serviceTypeEnum.pet;
  pet = serviceTypeEnum.pet;
  vet = serviceTypeEnum.vet

  constructor(private http: HttpClient, private bsModalService: BsModalService) { }

  ngOnInit(): void {
    this.getServices(this.tableDate);
  }

  getServices(date: string) {
    this.subLIst.push(this.http.get<ServiceModel[]>(`api/v1/vetservices/date?date=${date}`)
      .pipe(
        tap(res => {
          this.vetDayList = res;
          this.vetDayList.map(item => item.scheduledDate = new Date(item.scheduledDate));
        }),
        catchError(err => {
          console.error(err);
          return of();
        })).subscribe()
    );

    this.subLIst.push(this.http.get<ServiceModel[]>(`api/v1/petservice/date?date=2025/04/06`)//${date}
      .pipe(
        tap(res => {
          this.petDayList = res;
          this.petDayList.map(item => item.scheduledDate = new Date(item.scheduledDate));
        }),
        catchError(err => {
          console.error(err);
          return of();
        })).subscribe()
    );
  }

  getServiceAtHour(hour: string, service: ServiceModel[]): any | null {
    if (service) {
      return service.find(service => {
        const h = service.scheduledDate.getHours().toString();
        const m = service.scheduledDate.getMinutes().toString().padStart(2, "0");
        return `${h}:${m}` === hour;
      }) || null;
    }
    return null;
  }

  getTypeString(type: PetTypeEnum) {
    switch (type) {
      case 1:
        return "Cachorro";
      case 2:
        return "Gato";
      default:
        return "Não informado";
    }
  }

  formatPhone(phone: string) {
    return Helper.formatPhoneHelper(phone);
  }

  expandSideBar() {
    const sideBar = document.querySelector('.side-menu');
    if (!sideBar?.classList.contains("expanded")) {
      sideBar?.classList.add("expanded");
    }
    else {
      sideBar.classList.remove("expanded");
    }
  }

  selectActiveTable(type: serviceTypeEnum) {
    this.typeService = type;
  }

  selectActive(element: MouseEvent) {
    Helper.selectActiveHandler(element);
  }

  openPetModal(serviceId:number) {
    console.log(serviceId);
    const initialState = {
      id: serviceId
    };
    
    this.bsModalRef = this.bsModalService.show(PetformComponent, { initialState: initialState }); 
  }

  openVetModal(id:number){
    //vai fazer o mesmo do de cima, precisa desenvolver o outro formulário.
  }

  ngOnDestroy(): void {
    this.subLIst.forEach(sub => sub.unsubscribe());
  }
}