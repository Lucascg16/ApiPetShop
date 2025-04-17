import { FormControl, FormGroup } from "@angular/forms";

export class FormValidator {
  static passwordValidate(control: FormControl) {
    const password = control.value;

    if (password && password !== '') {
      const passwordRule = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[\W_]).{8,}$/;
      return passwordRule.test(password) ? null : { invalidPassword: true };
    }
    return null;
  }

  static confirmPasswordValidator(password: string) {
    const validator = (formControl: FormControl) => {
      if (password == null) {
        throw new Error('É necessário informar a senha');
      }

      if (!formControl.root || !(<FormGroup>formControl.root).controls) {
        return null;
      }

      const field = (<FormGroup>formControl.root).get(password);

      if (!field) {
        throw new Error('É necessário informar um campo válido.');
      }

      if (field.value !== formControl.value) {
        return { InvalidPasswordConfirmation: password };
      }

      return null;
    };
    return validator;
  }

  static validatePhoneNumber(control: FormControl){
    const phoneNumber = control.value;

    if(phoneNumber){
      const phoneRule = /^\([1-9]{2}\) (?:[2-8]|9[0-9])[0-9]{3}\-[0-9]{4}$/
      return phoneRule.test(phoneNumber) ? null : { invalidPhoneNumber: true }
    }
    return null;
  }

  static getErrorMsg(fieldName: string, validatorName: string, validatorValue?: any) {
    const config: any = {
      'required': `${fieldName} é obrigatório`,
      'minlength': `${fieldName} precisa ter no mínimo ${validatorValue.requiredLength}`,
      'maxlength': `${fieldName} pode no máximo ter ${validatorValue.requiredLength}`,
      'email': `O email digitado não é válido`,
      'invalidPassword': `A senha digitada é invalida, favor verificar as regras de senha no card`,
      'InvalidPasswordConfirmation': 'As senhas digitadas devem ser iguais',
      'invalidPhoneNumber': 'O numero de telefone digitado não é válido'
    }

    return config[validatorName];
  }
}