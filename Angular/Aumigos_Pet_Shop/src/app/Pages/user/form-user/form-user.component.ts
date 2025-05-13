import { Component, OnDestroy, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { ApiServices } from '../../../Services/petShopApi.service';
import { FormBuilder, FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { UserEnum } from '../../../Model/enum/shopEnum.enum';
import { UserModel } from '../../../Model/user.model';
import { sessionModel } from '../../../Model/sessionModel.model';

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
  loading: boolean = true;
  sending: boolean = false;

  userenum = UserEnum;
  currentuser: sessionModel;

  constructor(public bsModalRef: BsModalRef, private apiservices: ApiServices, private formbuilder: FormBuilder) {
    super();

    this.form = this.formbuilder.group({
      id: 0,
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      email: [null, [Validators.required, Validators.email]],
      role: new FormControl({ value: 0, disabled: false }, [Validators.required])
    });
  }

  ngOnInit(): void {
    this.currentuser = JSON.parse(sessionStorage.getItem('currentUser') as string)

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

    if(this.currentuser.id === this.id){
      this.form.get('role')?.disable();
    }    
  }

  override async submit() {
    this.sending = true;

    let modelbody: UserModel = this.form.value;

    try {
      if (modelbody.id === 0) {
        await this.apiservices.post("api/v1/users", modelbody);
      }
      else {
        await this.apiservices.patch("api/v1/users", modelbody)
      }

      this.sending = false;
      this.alertmsg = { message: "Usuário salvo com sucesso a página será recarregada", isSuccess: true };

      setTimeout(() => {
        window.location.reload()
      }, 1000);
    }
    catch (err) {
      this.sending = false;
      console.error(err);
      this.alertmsg = { message: "Ocorreu algum erro, tente novamente mais tarde ou contate um administrador", isSuccesse: false };
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
