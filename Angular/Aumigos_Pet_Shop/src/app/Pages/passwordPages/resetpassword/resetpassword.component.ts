import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { FormValidator } from '../../../Shared/base-form/form-validator';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ApiServices } from '../../../Services/petShopApi.service';



@Component({
  selector: 'app-resetpassword',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent],
  templateUrl: './resetpassword.component.html',
  styleUrl: './resetpassword.component.css'
})
export class ResetpasswordComponent extends BaseFormComponent implements OnDestroy, OnInit {
  sublist: Subscription[] = []
  alertmsg: any;
  sending:boolean = false;
  id: number;
  jwthelper = new JwtHelperService();

  constructor(private formbuilder: FormBuilder, private apiservice:ApiServices, private route: ActivatedRoute, private router: Router) {
    super();

    this.form = formbuilder.group({
      password: [null, [Validators.required, FormValidator.passwordValidate]],
      confirmpassword: [null, [Validators.required, FormValidator.confirmPasswordValidator('password')]]
    })
  }

  ngOnInit(): void {
    let token: string = "";

    this.sublist.push(this.route.queryParams.subscribe(param => {
      token = param['Token']
    }));

    if (this.jwthelper.isTokenExpired(token)) {
      this.alertmsg = { message: "Token expirado, favor solicitar novamente a troca de senha", isSuccess: false };
    }

    var decodedToken: any = jwtDecode(token);
    this.id = Number.parseInt(decodedToken.id);
  }

  override async submit() {
    this.sending = true;
    try{
      await this.apiservice.patch('api/v1/users/reset', { Id: this.id, Password: this.form.get('password')?.value });
      this.alertmsg = { message: "Senha alterada", isSuccess: true };
      this.sending = false;
    }
    catch(err){
      this.alertmsg = { message: "Ocorreu algum erro, tente novamente mais tarde", isSuccess: false };
      console.error(err);
      this.sending = false;
    }
  }

  ngOnDestroy(): void {
    this.sublist.forEach(sub => sub.unsubscribe());
  }
}
