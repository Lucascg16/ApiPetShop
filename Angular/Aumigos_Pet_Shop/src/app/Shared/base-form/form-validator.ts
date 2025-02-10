import { FormControl } from "@angular/forms";

export class FormValidator{
    static passwordValidate(control: FormControl){
        const password = control.value;

        if(password && password !== ''){
            const passwordRule = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[\W_]).{8,}$/;
            return passwordRule.test(password) ? null : { invalidPassword : true };
        }
        return null;
    }

    static getErrorMsg(fieldName: string, validatorName: string, validatorValue?: any){
        const config: any = {
            'required': `${fieldName} é obrigatório`,
            'minlength': `${fieldName} precisa ter no mínimo ${validatorValue.requiredLength}`,
            'maxlength': `${fieldName} pode no máximo ter ${validatorValue.requiredLength}`,
            'email': `O email digitado não é válido`,
            'invalidPassword': `A senha digitada é invalida, favor verificar as regras de senha no card`
        }

        return config[validatorName];
    }
}