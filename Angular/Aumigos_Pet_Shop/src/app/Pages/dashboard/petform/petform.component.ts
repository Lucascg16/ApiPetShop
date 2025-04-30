import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from '../../../Model/enum/shopEnum.enum';
import { ErroMsgComponent } from '../../../Shared/erro-msg/erro-msg.component';
import { Subscription } from 'rxjs';
import { PetserviceModel } from '../../../Model/Petservice.model';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Helper } from '../../../Shared/helper';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { ApiServices } from '../../../Services/petShopApi.service';

@Component({
  selector: 'app-petform',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent, ErroMsgComponent, FormsModule],
  templateUrl: './petform.component.html',
  styleUrl: './petform.component.css'
})

export class PetformComponent extends BaseFormComponent implements OnDestroy, OnInit, IBaseModal {
  id: number;
  date: string;
  alertMsg: any;
  loading: boolean = false;
  sending: boolean = false;

  subList: Subscription[] = []
  disableEmail: boolean = true;//a tela abre com o telefone selecionado.
  disablePhone: boolean = false;

  type = PetTypeEnum;
  petGender = PetGenderEnum;
  petsize = PetSizeEnum;
  schedulerTimes: string[];

  constructor(public bsModalRef: BsModalRef, private services: ApiServices, private formbuilder: FormBuilder) {
    super();

    this.form = formbuilder.group({
      id: 0,
      name: [null, Validators.required],
      contactMethod: 1,
      email: [null, Validators.email],
      phoneNumber: [null],
      isWhatsApp: false,
      petName: [null, Validators.required],
      petAge: [null, Validators.required],
      petType: [null, Validators.required],
      petGender: [null, Validators.required],
      petSize: [null, Validators.required],
      scheduledDate: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.subList.push(this.services.get<string[]>(`api/v1/availableTimes/pet?date=${this.date}`).subscribe(data => {
      this.schedulerTimes = data ?? []
    }));

    if (this.id !== 0) {
      this.loading = true;
      this.subList.push(this.services.get<PetserviceModel>(`api/v1/petservice?id=${this.id}`).subscribe(res => {
        let sched = new Date(res.scheduledDate);
        this.schedulerTimes.push(`${sched.getHours().toString().padStart(2, '0')}:${sched.getMinutes().toString().padStart(2, '0')}`);
        this.populateFormFields(res);
        this.loading = false;
      })
      );
    }
  }

  override async submit() {
    this.sending = true;

    if (this.form.get('isWhatApp')?.value == undefined) {
      this.form.patchValue({ isWhatApp: false });
    }

    let modelbody: PetserviceModel = this.form.value;
    modelbody.scheduledDate = `${this.date.replaceAll("/", "-")}T${this.form.get("scheduledDate")?.value}:00`;
    modelbody.phoneNumber = modelbody.phoneNumber?.replace("(", "").replace(")", "").replaceAll(" ", "").replace("-", "");

    console.log(modelbody.scheduledDate);
    
    try{
      if(modelbody.id === 0){
        await this.services.post("api/v1/petservice", modelbody);
      }else{
        await this.services.patch("api/v1/petservice", modelbody);
      }
      this.sending = false;
      this.alertMsg = {message: "Agendamento salvo com sucesso", isSuccess: true};

      setTimeout(() => {
        window.location.reload()
      }, 1000);
    }catch (error){
      this.sending = false;
      console.error(error);
      this.alertMsg = {message: "Ocorreu algum erro, tente novamente mais tarde ou contate um administrador", isSuccesse: false };
    }
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

  populateFormFields(service: PetserviceModel) {
    this.resetFormData();
    let serviceDate = new Date(service.scheduledDate);

    this.form.patchValue({
      id: service.id,
      name: service.name,
      email: service.email,
      phoneNumber: Helper.formatPhoneHelper(service.phoneNumber as string),
      isWhatsApp: service.isWhatsApp,
      petName: service.petName,
      petAge: service.petAge,
      petType: service.petType,
      petGender: service.petGender,
      petSize: service.petSize,
      scheduledDate: `${serviceDate.getHours().toString().padStart(2, '0')}:${serviceDate.getMinutes().toString().padStart(2, '0')}`
    })
  }

  resetFormData() {
    this.form.patchValue({
      id: 0,
      name: null,
      email: null,
      phoneNumber: null,
      isWhatsApp: false,
      petName: null,
      petAge: null,
      type: null,
      petGender: null,
      petSize: null,
      scheduledDate: null
    })
  }

  closeModal() {
    this.bsModalRef.hide()
  }

  ngOnDestroy(): void {
    this.subList.forEach(sub => sub.unsubscribe());
  }
}