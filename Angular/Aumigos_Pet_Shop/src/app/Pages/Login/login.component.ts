import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../../Shared/input-field/input-field.component';
import { BaseFormComponent } from '../../Shared/base-form/base-form';
import { AuthenticationService } from '../../Services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent extends BaseFormComponent {
  alertError: string;

  constructor(private formBuider: FormBuilder, private authService: AuthenticationService, private router: Router) {
    super();

    try{
      if(sessionStorage?.getItem('currentUser')){
        this.router.navigate(['/dashboard'])
      }
    }catch{}

    this.form = this.formBuider.group({
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required]]
    });
  }

  async submit() {
    let response = await this.authService.login(this.form.get('email')?.value, this.form.get('password')?.value);

    if(response.isSuccess){
      this.router.navigate(['/dashboard']);
    }
    else{
      console.log(response.response.message);
      this.alertError = response.response.message;
    }
  }

  rediredForgotPass(){
    this.router.navigate(['/forgot']);
  }
}