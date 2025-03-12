import { Component } from '@angular/core';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseFormComponent } from '../../../Shared/base-form/base-form.component';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [InputFieldComponent, ReactiveFormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent extends BaseFormComponent{
  
  constructor(private formBuilder: FormBuilder){
    super();

    this.form = this.formBuilder.group({
      email: [null, [Validators.email, Validators.required]]
    })
  }

  override submit() {
    throw new Error('Method not implemented.');
  }
}
