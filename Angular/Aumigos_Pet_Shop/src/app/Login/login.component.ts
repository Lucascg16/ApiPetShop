import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../Shared/input-field/input-field.component';
import { BaseFormComponent } from '../Shared/base-form/base-form.component';
import { FormValidator } from '../Shared/base-form/form-validator';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent extends BaseFormComponent {  
  constructor(private formBuider: FormBuilder) { 
    super();

    this.form = this.formBuider.group({
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required, FormValidator.passwordValidate]]
    });
  }

  override submit() {
    //não faz nada ainda
  }
}