import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ServiceModel } from '../../Model/ServiceModel.model';
import { Helper } from '../../Shared/helper';
import { PetformComponent } from './petform/petform.component';
import { PetTypeEnum, serviceTypeEnum } from '../../Model/enum/shopEnum.enum';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgbDatepickerModule, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { VetformComponent } from './vetform/vetform.component';
import { ApiServices } from '../../Services/petShopApi.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [NgbDatepickerModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit, OnDestroy {
  subLIst: Subscription[] = [];
  petDayList: ServiceModel[];
  vetDayList: ServiceModel[];
  hourList = ["08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30"];
  tableDate: string = new Date().toLocaleDateString('ja-JP', { year: 'numeric', month: '2-digit', day: '2-digit' }).toString();
  bsModalRef?: BsModalRef;
  date: NgbDateStruct = { year: new Date().getUTCFullYear(), month: new Date().getUTCMonth() + 1, day: new Date().getUTCDate() };

  typeService: serviceTypeEnum = serviceTypeEnum.pet;
  pet = serviceTypeEnum.pet;
  vet = serviceTypeEnum.vet
  loading: boolean = true;

  constructor(private apiservice: ApiServices, private bsModalService: BsModalService) { }

  ngOnInit(): void {
    this.getServices(this.tableDate);
  }

  loadDayDataTable(date: NgbDateStruct) {
    this.tableDate = `${date.year}/${date.month.toString().padStart(2, '0')}/${date.day.toString().padStart(2, '0')}`;
    this.getServices(this.tableDate);
  }

  getServices(date: string) {
    this.loading = true;

    try{
      this.subLIst.push(
        this.apiservice.get<ServiceModel[]>(`api/v1/vetservices/date?date=${date}`).subscribe(
          res => {
            this.vetDayList = res;
            this.vetDayList.map(item => item.scheduledDate = new Date(item.scheduledDate));
          }
        ),
        this.apiservice.get<ServiceModel[]>(`api/v1/petservice/date?date=${date}`).subscribe(
          res => {
            this.petDayList = res;
            this.petDayList.map(item => item.scheduledDate = new Date(item.scheduledDate));
            this.loading = false;
          }
        )
      );
    }catch (error){
      this.loading = false;
      console.error(error);
    }
  }

  getServiceAtHour(hour: string, service: ServiceModel[]): any | null {
    if (service) {
      return service.find(service => {
        const h = service.scheduledDate.getHours().toString().padStart(2, '0');
        const m = service.scheduledDate.getMinutes().toString().padStart(2, '0');
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

  openPetModal(serviceId: number) {
    const initialState = {
      id: serviceId,
      date: this.tableDate
    };

    this.bsModalRef = this.bsModalService.show(PetformComponent, { initialState: initialState });
  }

  openVetModal(serviceId: number) {
    const initialState = {
      id: serviceId,
      date: this.tableDate
    };

    this.bsModalRef = this.bsModalService.show(VetformComponent, { initialState: initialState });
  }

  ngOnDestroy(): void {
    this.subLIst.forEach(sub => sub.unsubscribe());
  }
}