import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Subscription, catchError, of, tap  } from 'rxjs';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [InputFieldComponent, ReactiveFormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent extends BaseFormComponent implements OnDestroy{
  httpsub: Subscription;
  successmsg: string
  erromsg: string;

  constructor(private formBuilder: FormBuilder, private http: HttpClient){
    super();

    this.form = this.formBuilder.group({
      email: [null, [Validators.email, Validators.required]]
    })
  }

  override submit() {
    this.erromsg = "";
    this.successmsg = "";
    
    this.httpsub =  this.http.post(`api/v1/email?userEmail=${this.form.get("email")?.value}`, {})
    .pipe(
      tap(res => this.successmsg = "Email enviado com sucesso, verifique a sua caixa de Spam"),
      catchError(er => {
        this.erromsg = er.error.error;
        return of();
      })
    )
    .subscribe();    
  }

  ngOnDestroy(): void {
    if(this.httpsub){
      this.httpsub.unsubscribe();
    }
  }
}
