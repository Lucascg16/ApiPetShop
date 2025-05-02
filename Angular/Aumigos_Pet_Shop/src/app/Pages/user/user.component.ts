import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApiServices } from '../../Services/petShopApi.service';
import { UserModel } from '../../Model/user.model';
import { UserEnum } from '../../Model/enum/shopEnum.enum';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormUserComponent } from './form-user/form-user.component';

@Component({
  selector: 'app-user',
  imports: [],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit, OnDestroy {
  sublist: Subscription[] = [];
  users: UserModel[] = [];
  bsModalRef?: BsModalRef;
  loading: boolean = false;

  constructor(private apiservice: ApiServices, private bsModalService: BsModalService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.loading = true;
    try {
      this.sublist.push(
        this.apiservice.get<UserModel[]>("api/v1/users/all").subscribe(
          res => {
            this.users = res;
          }
        )
      );
      this.loading = false;
    }
    catch (err) {
      console.error(err);
      this.loading = false;
    }
  }

  getUserenum(userenum: UserEnum): string {
    switch (userenum) {
      case 1:
        return "Administrador";
      case 2:
        return "Funcionário"
      case 3:
        return "Usuário";
      default:
        return "Nenhum";
    }
  }

  openFormModal(userId: number){
    const initialState = {
      id: userId
    }

    this.bsModalRef = this.bsModalService.show(FormUserComponent, { initialState: initialState })
  }

  ngOnDestroy(): void {
    this.sublist.forEach(sub => sub.unsubscribe());
  }
}
