import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ApiServices } from '../../../Services/petShopApi.service';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';

@Component({
  selector: 'app-companie',
  imports: [FormsModule, ReactiveFormsModule, InputFieldComponent],
  templateUrl: './companie.component.html',
  styleUrl: './companie.component.css'
})
export class CompanieComponent extends BaseFormComponent implements OnInit, OnDestroy, IBaseModal {
  loading: boolean = false;
  sending: boolean = false;
  alertmsg: any;

  constructor(public bsmodalref: BsModalRef, private formbuilder: FormBuilder, private apiservice: ApiServices) {
    super();

    this.form = formbuilder.group({
      name: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      address: formbuilder.group({
        id: null,
        street: [null, Validators.required],
        city: [null, Validators.required],
        state: [null, Validators.required],
        neighborhood: [null, Validators.required],
        zipCode: [null, Validators.required]
      }),
      phoneNumber: [null, Validators.required],
      instagramAddress: [null, Validators.required]
    });
  }

  ngOnInit(): void {

  }

  override submit() {
    throw new Error('Method not implemented.');
  }

  closeModal(): void {
    this.bsmodalref.hide();
  }

  ngOnDestroy(): void {

  }
}
