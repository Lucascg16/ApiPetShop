import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { Subscription } from 'rxjs';
import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from '../../../Model/enum/shopEnum.enum';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormServices } from '../form.service';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { ErroMsgComponent } from '../../../Shared/erro-msg/erro-msg.component';
import { vacineModel } from '../../../Model/vacine.model';
import { VetServiceModel } from '../../../Model/vetService.model';
import { Helper } from '../../../Shared/helper';

@Component({
  selector: 'app-vetform',
  imports: [ReactiveFormsModule, InputFieldComponent, ErroMsgComponent, FormsModule],
  templateUrl: './vetform.component.html',
  styleUrl: './vetform.component.css'
})
export class VetformComponent extends BaseFormComponent implements OnInit, OnDestroy, IBaseModal {
  id: number;
  date: string;
  alertmsg: string;
  loading: boolean = false;
  sending: boolean = false;

  subList: Subscription[] = [];
  disableEmail: boolean = true;
  disablePhone: boolean = false;

  type = PetTypeEnum;
  petGender = PetGenderEnum;
  petSize = PetSizeEnum;
  schedulerTimes: string[] = [];

  available: vacineModel[] = [];
  selected: vacineModel[] = [];

  constructor(public bsModalRef: BsModalRef, private services: FormServices, private formbuilder: FormBuilder) {
    super();

    this.form = formbuilder.group({
      id: 0,
      name: [null, Validators.required],
      contactMethod: 1,
      email: [null, Validators.email],
      phoneNumber: null,
      isWhatsApp: false,
      petName: [null, Validators.required],
      petAge: [null, Validators.required],
      petType: [null, Validators.required],
      petGender: [null, Validators.required],
      petSize: [null, Validators.required],
      scheduledDate: [null, Validators.required],
      petWeight: [null, Validators.required],
      isCastrared: false,
      vacines: null,
    });
  }

  override submit() { }

  ngOnInit(): void {
    this.subList.push(this.services.getAvailableTimes(`api/v1/availableTimes/vet?date=${this.date}`).subscribe(data => {
      this.schedulerTimes = data
    }));

    this.subList.push(this.services.getAnyData<vacineModel[]>('api/v1/vacines').subscribe(data => {
      this.available = data ?? [];
    }))

    if(this.id !== 0){
      this.loading = true;
      
      this.subList.push(
        this.services.getAnyData<VetServiceModel>(`api/v1/vetservices?id=${this.id}`)
        .subscribe(res => {
          let sched = new Date(res.scheduledDate);
          this.schedulerTimes.push(`${sched.getHours()}:${sched.getMinutes().toString().padStart(2, '0')}`);
          this.populateFormFields(res);
          this.selectServiceVacines(res.vacines);
          this.loading = false;
        })
      );
    }
  }

  selectServiceVacines(vacines: vacineModel[]){
    vacines.forEach(vacine => this.selected.push(vacine));
    this.available = this.available.filter(item => !vacines.includes(item));
  }

  verifyContact() {
    this.disableEmail = false;
    this.disablePhone = false;

    if (this.form.get('contactMethod')?.value === 1) {
      this.disableEmail = true;
    }
    if (this.form.get('contactMethod')?.value === 2) {
      this.disablePhone = true;
    }
  }

  populateFormFields(service: VetServiceModel) {
    this.resetFormData();
    let serviceDate = new Date(service.scheduledDate);
    console.log(serviceDate);

    this.form.patchValue({
      id: service.id,
      name: service.name,
      email: service.email,
      phoneNumber: Helper.formatPhoneHelper(service.phoneNumber as string) ??  null,
      isWhatsApp: service.isWhatsApp,
      petName: service.petName,
      petAge: service.petAge,
      petType: service.petType,
      petGender: service.petGender,
      petSize: service.petSize,
      scheduledDate: `${serviceDate.getHours()}:${serviceDate.getMinutes().toString().padStart(2, '0')}`,
      petWeight: service.petWeight,
      isCastrared: service.isCastrated,
      //vacines: null, verificar a forma que será passado para as tabelas
    });
  }

  resetFormData() {
    this.form.patchValue({
      id: 0,
      name: null,
      contatctMethod: 1,
      email: null,
      phoneNumber: null,
      isWhatsApp: false,
      petName: null,
      petAge: null,
      petType: null,
      petGender: null,
      petSize: null,
      scheduledDate: null,
      petWeight: null,
      isCastrared: false,
      vacines: null
    });
  }

  moverParaSelecionados() {
    this.selected.push(...this.form.get('vacines')?.value);
    this.available = this.available.filter(item => !this.form.get('vacines')?.value.includes(item));
    this.form.patchValue({vacines: null});
  }

  moverParaDisponiveis() {
    this.available.push(...this.form.get('vacines')?.value);
    this.selected = this.selected.filter(item => !this.form.get('vacines')?.value.includes(item));
    this.form.patchValue({vacines: null});
  }

  closeModal(): void {
    this.bsModalRef.hide();
  }

  ngOnDestroy(): void {
    this.subList.forEach(sub => sub.unsubscribe());
  }
}
