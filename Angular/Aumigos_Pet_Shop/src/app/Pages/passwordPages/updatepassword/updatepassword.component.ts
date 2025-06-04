import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiServices } from '../../../Services/petShopApi.service';
import { FormValidator } from '../../../Shared/base-form/form-validator';
import { sessionModel } from '../../../Model/sessionModel.model';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';

@Component({
  selector: 'app-updatepassword',
  imports: [ReactiveFormsModule, FormsModule, InputFieldComponent],
  templateUrl: './updatepassword.component.html',
  styleUrl: './updatepassword.component.css'
})
export class UpdatepasswordComponent extends BaseFormComponent implements OnInit, IBaseModal {
  id: number;
  alertmsg: any;
  sending: boolean = false;

  constructor(private formbuilder: FormBuilder, private apiservice: ApiServices, public bsModalRef: BsModalRef) {
    super()

    this.form = formbuilder.group({
      password: [null, Validators.required],
      newPassword: [null, [Validators.required, FormValidator.passwordValidate]],
      confirmNewPassword: [null, [Validators.required, FormValidator.confirmPasswordValidator('newPassword')]]
    });
  }

  ngOnInit(): void {
    let currentUser: sessionModel = JSON.parse(sessionStorage.getItem('currentUser') as string)
    this.id = currentUser.id;
  }

  override async submit() {
    this.sending = true;
    let modelbody = {
      id: this.id,
      password: this.form.get('password'),
      newPassword: this.form.get('newPassword')
    }

    try {
      this.apiservice.patch("/api/v1/users/password", modelbody);

      this.alertmsg = { message: "Senha alterada", isSuccess: true };
      this.sending = false
    }
    catch (err) {
      console.error(err);
      this.alertmsg = { message: "Ocurreu um erro, tente novamente mais tarde", isSuccess: false };
      this.sending = false
    }
  }

  closeModal(): void {
    this.bsModalRef.hide()
  }
}
