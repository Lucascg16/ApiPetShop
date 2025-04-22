import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { Subscription } from 'rxjs';
import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from '../../../Model/enum/shopEnum.enum';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { HttpClient } from '@angular/common/http';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-vetform',
  imports: [],
  templateUrl: './vetform.component.html',
  styleUrl: './vetform.component.css'
})
export class VetformComponent extends BaseFormComponent implements OnInit, OnDestroy, IBaseModal {
  id: number;
  date: string;
  alertmsg: string;
  loaging: boolean = false;
  sending: boolean = false;

  subList: Subscription[] = [];
  disableEmail: boolean = true;
  disablePhone: boolean = false;

  type = PetTypeEnum;
  petGender = PetGenderEnum;
  petSize = PetSizeEnum;
  schedulerTimes: string[];
  
  constructor(public bsModalRef: BsModalRef, private http: HttpClient, private formbuilder: FormBuilder) {
    super();
  }
  
  override submit() {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {

  }

  getAvailableTimes(){
    this.subList.push(this.http.get<string[]>("").subscribe())
  }

  closeModal(): void {
    this.bsModalRef.hide();
  }
  
  ngOnDestroy(): void {
    this.subList.forEach(sub => sub.unsubscribe());
  }
}
