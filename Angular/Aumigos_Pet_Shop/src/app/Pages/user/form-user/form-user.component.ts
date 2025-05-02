import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { ApiServices } from '../../../Services/petShopApi.service';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { UserEnum } from '../../../Model/enum/shopEnum.enum';
import { UserModel } from '../../../Model/user.model';

@Component({
  selector: 'app-form-user',
  imports: [ReactiveFormsModule, InputFieldComponent, FormsModule],
  templateUrl: './form-user.component.html',
  styleUrl: './form-user.component.css'
})
export class FormUserComponent extends BaseFormComponent implements OnInit, OnDestroy, IBaseModal {
  id: number;
  sublist: Subscription[] = [];
  alertmsg: any;
  loading: boolean = false;
  sending: boolean = false;

  userenum = UserEnum;

  constructor(public bsModalRef: BsModalRef, private apiservices: ApiServices, private formbuilder: FormBuilder) {
    super();

    this.form = this.formbuilder.group({
      id: 0,
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      role: [0, Validators.required]
    });
  }

  override submit() {

  }

  ngOnInit(): void {
    if (this.id !== 0) {
      this.loading = true;
      try {
        this.sublist.push(
          this.apiservices.get<UserModel>(`api/v1/users?id=${this.id}`).subscribe(
            res => {
              this.pupulateFormFields(res);
              this.loading = false;
            }
          )
        )
      }
      catch (err) {
        console.error(err);
        this.loading = false;
      }
    }
  }

  pupulateFormFields(user: UserModel) {
    this.form.patchValue({
      id: user.id,
      firstName: user.firstName,
      lastName: user.lastName,
      email: user.email,
      role: user.role
    })
  }

  resetFormData() {
    this.form.patchValue({
      id: 0,
      firstName: null,
      lastName: null,
      email: null,
      role: 0
    })
  }

  closeModal() {
    this.bsModalRef.hide();
  }

  ngOnDestroy(): void {
    this.sublist.forEach(sub => sub.unsubscribe());
  }
}
