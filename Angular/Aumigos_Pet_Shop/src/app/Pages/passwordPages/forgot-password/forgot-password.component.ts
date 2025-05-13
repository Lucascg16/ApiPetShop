import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { ApiServices } from '../../../Services/petShopApi.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [InputFieldComponent, ReactiveFormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent extends BaseFormComponent implements OnDestroy {
  httpsub: Subscription;
  alertmsg: any;
  sending: boolean = false;

  constructor(private formBuilder: FormBuilder, private apiservice: ApiServices) {
    super();

    this.form = this.formBuilder.group({
      email: [null, [Validators.email, Validators.required]]
    })
  }

  override submit() {
    this.sending = true;
    try {
      this.apiservice.post(`api/v1/email?userEmail=${this.form.get("email")?.value}`, {});
      this.alertmsg = { message: "Email enviado com sucesso, verifique a sua caixa de Spam", isSuccess: true };
      this.sending = false;
    }
    catch (err) {
      this.alertmsg = { message: "Ocorreu um erro, tente novamente mais tarde", isSuccess: false };
      console.error(err);
      this.sending = false;
    }
  }

  ngOnDestroy(): void {
    if (this.httpsub) {
      this.httpsub.unsubscribe();
    }
  }
}
