import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form.component';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from '../../../Model/enum/shopEnum.enum';
import { ErroMsgComponent } from '../../../Shared/erro-msg/erro-msg.component';
import { catchError, of, Subscription, tap } from 'rxjs';
import { PetserviceModel } from '../../../Model/Petservice.model';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Helper } from '../../../Shared/helper';

@Component({
  selector: 'app-petform',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent, ErroMsgComponent, FormsModule],
  templateUrl: './petform.component.html',
  styleUrl: './petform.component.css'
})

export class PetformComponent extends BaseFormComponent implements OnDestroy, OnInit{
  id: number;
  date: string;
  
  subList: Subscription[] = []
  disableEmail: boolean = true;//a tela abre  com o telefone selecionado.
  disablePhone: boolean = false;

  type = PetTypeEnum;
  petGender = PetGenderEnum;
  petsize = PetSizeEnum;
  schedulerTimes: string[];

  constructor(public bsModalRef: BsModalRef, private http: HttpClient, private formbuilder: FormBuilder) {
    super();

    this.form = formbuilder.group({
      id: null,
      name: [null, [Validators.required]],
      contactMethod: [1],
      email: [null, [Validators.email]],
      phone: [null], //desenvolver o validador de telefone
      isWpp: [false],
      petName: [null, Validators.required],
      petAge: [null, Validators.required],
      type: [null, Validators.required],
      petGender: [null, Validators.required],
      petSize: [null, Validators.required],
      scheduler: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    if(this.id !== 0){
      this.getServiceData();
    }
  }

  override submit() { }

  verifyContact() {
    console.log(this.form.get('contactMethod')?.value);
    this.disableEmail = false;
    this.disablePhone = false;

    if (this.form.get('contactMethod')?.value === 1) {
      this.disableEmail = true;
    }
    if (this.form.get('contactMethod')?.value === 2) {
      this.disablePhone = true;
    }

  }

  getAvailableTimes() {
    this.subList.push(this.http.get<string[]>(`api/v1/availableTimes/pet?date=${this.date}`)
      .pipe(
        tap(res => this.schedulerTimes = res),
        catchError(err => {
          console.error(err);
          return of();
        })
      ).subscribe()
    );
  }

  getServiceData() {
    this.subList.push(this.http.get<PetserviceModel>(`api/v1/petservice?id=${this.id}`)
      .pipe(
        tap(res => this.populateFormFields(res)),
        catchError(err => {
          console.error(err);
          return of();
        })
      ).subscribe()
    );
  }

  populateFormFields(service: PetserviceModel) {
    this.resetFormData();
    let serviceDate = new Date(service.scheduledDate);

    this.form.patchValue({
      id: service.id,
      name: service.name,
      email: service.email,
      phone: Helper.formatPhoneHelper(service.phoneNumber as string),
      isWpp: service.isWhatApp,
      petName: service.petName,
      petAge: service.petAge,
      type: service.petType,
      petGender: service.petGender,
      petSize: service.petSize,
      scheduler: `${serviceDate.getHours()}:${serviceDate.getMinutes()}`
    })
  }

  resetFormData() {
    this.form.patchValue({
      id: null,
      name: null,
      email: null,
      phone: null,
      isWpp: false,
      petName: null,
      petAge: null,
      type: null,
      petGender: null,
      petSize: null,
      scheduler: null
    })
  }

  closeModal(){
    this.bsModalRef.hide()
  }

  ngOnDestroy(): void {
    this.subList.forEach(sub => sub.unsubscribe());
  }
}
