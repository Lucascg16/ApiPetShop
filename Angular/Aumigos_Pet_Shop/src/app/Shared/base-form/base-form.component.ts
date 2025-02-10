import { Component } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';;

@Component({
  selector: 'app-base-form',
  standalone: true,
  imports: [],
  template: '<div></div>'
})
export abstract class BaseFormComponent {
  form: FormGroup;
  abstract submit(): any;

  constructor(){}

  onSub(){
    if(this.form.valid){
      this.submit();
    }
    else{
      this.verifyFormValidations(this.form);
    }
  }

  verifyFormValidations(form: FormGroup | FormArray){
    Object.keys(form.controls).forEach(field => {
      const formControl = form.get(field);

      formControl?.markAsDirty();
      formControl?.markAsTouched();

      if(formControl instanceof FormGroup || formControl instanceof FormArray){
        this.verifyFormValidations(formControl);
      }
    })
  }

  resetFormSubmit(){
    this.form.reset();
  }

  verifyValidTouched(field: string){
    return !this.form.get(field)?.valid && this.form.get(field)?.touched;
  }

  verifyEmail(){
    let email = this.form.get('email');
    if(email?.errors){
      return email.errors['email'] && email.touched;
    }
  }

  aplyCssError(field: string){
    return {
      'has-error': this.verifyValidTouched(field),
      'has-feedback': this.verifyValidTouched(field)
    }
  }

  getField(field: string){
    return this.form.get(field);
  }
}
