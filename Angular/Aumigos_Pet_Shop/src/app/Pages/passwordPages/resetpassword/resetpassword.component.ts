import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { catchError, of, Subscription, tap } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { FormValidator } from '../../../Shared/base-form/form-validator';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { BaseFormComponent } from '../../../Shared/base-form/base-form';
import { JwtHelperService } from '@auth0/angular-jwt';



@Component({
  selector: 'app-resetpassword',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent],
  templateUrl: './resetpassword.component.html',
  styleUrl: './resetpassword.component.css'
})
export class ResetpasswordComponent extends BaseFormComponent implements OnDestroy, OnInit {
  httpsub: Subscription; routesub: Subscription;
  successmsg: string
  erromsg: string;
  id: number;
  jwthelper = new JwtHelperService();

  constructor(private formbuilder: FormBuilder, private http: HttpClient, private route: ActivatedRoute, private router: Router) {
    super();

    this.form = formbuilder.group({
      password: [null, [Validators.required]],
      confirmpassword: [null, [Validators.required, FormValidator.confirmPasswordValidator('password')]]
    })
  }

  ngOnInit(): void {
    let token: string = "";

    this.routesub = this.route.queryParams.subscribe(param => {
      token = param['Token']
    });

    if(this.jwthelper.isTokenExpired(token)){
      this.router.navigate(["/home"]);
    }

    var decodedToken: any = jwtDecode(token);
    this.id = Number.parseInt(decodedToken.id);
  }

  override submit() {
    this.successmsg = "";
    this.erromsg = "";

    this.httpsub = this.http.patch('api/v1/users/reset', {Id: this.id, Password: this.form.get('password')?.value})
    .pipe(
      tap(res => this.successmsg = "Senha alterada com sucesso"),
      catchError(er => {
        this.erromsg = "Algo deu errado, tente novamente ou entre em contato com o administrador";
        return of();
      })
    )
    .subscribe();
  }

  ngOnDestroy(): void {
    if (this.httpsub) {
      this.httpsub.unsubscribe();
    }
    if (this.routesub) {
      this.routesub.unsubscribe();
    }
  }
}
