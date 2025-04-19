import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
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

export class PetformComponent extends BaseFormComponent implements OnDestroy, OnInit {
  id: number;
  date: string;
  alertMsg: string;

  subList: Subscription[] = []
  disableEmail: boolean = true;//a tela abre com o telefone selecionado.
  disablePhone: boolean = false;

  type = PetTypeEnum;
  petGender = PetGenderEnum;
  petsize = PetSizeEnum;
  schedulerTimes: string[];

  constructor(public bsModalRef: BsModalRef, private http: HttpClient, private formbuilder: FormBuilder) {
    super();

    this.form = formbuilder.group({
      id: 0,
      name: [null, [Validators.required]],
      contactMethod: [1],
      email: [null, Validators.email],
      phoneNumber: [null],
      isWhatApp: [false],
      petName: [null, Validators.required],
      petAge: [null, Validators.required],
      petType: [null, Validators.required],
      petGender: [null, Validators.required],
      petSize: [null, Validators.required],
      scheduledDate: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.getAvailableTimes();
    if (this.id !== 0) {
      this.getServiceData();
    }
  }

  override submit() {
    if(this.form.get('isWhatApp')?.value == undefined){
      this.form.patchValue({isWhatApp: false});
    }

    let modelbody: PetserviceModel = this.form.value;
    modelbody.scheduledDate = `${this.date.replaceAll("/","-")}T${this.form.get("scheduledDate")?.value}`;
    modelbody.phoneNumber = modelbody.phoneNumber?.replace("(", "").replace(")", "").replaceAll(" ", "").replace("-", "");

    if(this.form.get('id')?.value as number === 0){
      this.subList.push(this.http.post("api/v1/petservice", modelbody, { responseType: 'text'})
      .pipe(
        tap(res => this.alertMsg = "Agendamento criado com sucesso"),
        catchError(err => {
          this.alertMsg = err.error;
          return of();
        })
      ).subscribe())
    }else{
      this.subList.push(this.http.patch("api/v1/petservice", modelbody)
      .pipe(
        tap(res => this.alertMsg = "Agendamento allterado com sucesso"),
        catchError(err => {
          this.alertMsg = err.error;
          return of();
        })
      ).subscribe()
    )
    }
  }

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
        tap(res => {
          let sched = new Date(res.scheduledDate)
          this.schedulerTimes.push(`${sched.getHours()}:${sched.getMinutes()}`);
          this.populateFormFields(res)
        }),
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
      phoneNumber: Helper.formatPhoneHelper(service.phoneNumber as string),
      isWhatApp: service.isWhatApp,
      petName: service.petName,
      petAge: service.petAge,
      petType: service.petType,
      petGender: service.petGender,
      petSize: service.petSize,
      scheduledDate: `${serviceDate.getHours()}:${serviceDate.getMinutes()}`
    })
  }

  resetFormData() {
    this.form.patchValue({
      id: null,
      name: null,
      email: null,
      phoneNumber: null,
      isWpp: false,
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
