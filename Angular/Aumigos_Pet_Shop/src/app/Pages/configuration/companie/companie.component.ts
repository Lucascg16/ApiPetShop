import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ApiServices } from '../../../Services/petShopApi.service';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { CompanyModel } from '../../../Model/CompanyModel.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-companie',
  imports: [FormsModule, ReactiveFormsModule, InputFieldComponent],
  templateUrl: './companie.component.html',
  styleUrl: './companie.component.css'
})
export class CompanieComponent extends BaseFormComponent implements OnInit, OnDestroy, IBaseModal {
  loading: boolean = true;
  sending: boolean = false;
  alertmsg: any;
  sub: Subscription; 

  constructor(public bsmodalref: BsModalRef, private formbuilder: FormBuilder, private apiservice: ApiServices) {
    super();

    this.form = formbuilder.group({
      id: null,
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
    this.sub = this.apiservice.get<CompanyModel>("api/v1/company").subscribe(res => {
      this.populateForm(res);
      this.loading = false;
    });
  }

  override async submit() {
    this.sending = true;
    let modelbody: CompanyModel = this.form.value;

    try {
      if (modelbody.id === 0) {
        await this.apiservice.post("api/v1/company", modelbody);
      } else {
        await this.apiservice.patch("api/v1/company", modelbody);
      }
      this.sending = false;
      this.alertmsg = { message: "Informações da empresa salvas com sucesso", isSuccess: true };

      setTimeout(() => {
        window.location.reload()
      }, 1000);
    }
    catch (err) {
      console.error(err);
      this.sending = false
      this.alertmsg = { message: "Ocorreu algum erro, tente novamente mais tarde ou contate um administrador", isSuccess: false };
    }
  }

  populateForm(model: CompanyModel) {
    this.form.patchValue({
      name: model.name,
      email: model.contactEmail,
      address: {
        id: model.address.id,
        street: model.address.street,
        city: model.address.city,
        state: model.address.state,
        neighborhood: model.address.neighborhood,
        zipCode: model.address.zipCode,
      },
      phoneNumber: model.phoneNumber,
      instagramAddress: model.instagramAddress,
    });
  }

  resetForm() {
    this.form.patchValue({
      name: null,
      email: null,
      address: {
        id: null,
        street: null,
        city: null,
        state: null,
        neighborhood: null,
        zipCode: null,
      },
      phoneNumber: null,
      instagramAddress: null,
    })
  }

  closeModal(): void {
    this.bsmodalref.hide();
  }

  ngOnDestroy(): void {
    if(this.sub){
      this.sub.unsubscribe();
    }
  }
}
