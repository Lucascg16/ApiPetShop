import { FormArray, FormGroup } from '@angular/forms';;

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
}
