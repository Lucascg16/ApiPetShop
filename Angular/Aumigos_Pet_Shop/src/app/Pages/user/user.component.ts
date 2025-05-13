import { Component, OnDestroy, OnInit } from '@angular/core';
import { debounceTime, distinctUntilChanged, Subscription, switchMap } from 'rxjs';
import { ApiServices } from '../../Services/petShopApi.service';
import { UserModel } from '../../Model/user.model';
import { UserEnum } from '../../Model/enum/shopEnum.enum';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FormUserComponent } from './form-user/form-user.component';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { DeleteuserComponent } from './deleteuser/deleteuser.component';

@Component({
  selector: 'app-user',
  imports: [ReactiveFormsModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit, OnDestroy {
  sublist: Subscription[] = [];
  users: UserModel[] = [];
  bsModalRef?: BsModalRef;
  loading: boolean = false;
  searchControl = new FormControl();

  constructor(private apiservice: ApiServices, private bsModalService: BsModalService) { }

  ngOnInit(): void {
    this.getUsers();
    this.onsearchUsers();
  }

  onsearchUsers() {
    this.sublist.push(this.searchControl.valueChanges
      .pipe(
        debounceTime(500),
        distinctUntilChanged(),
        switchMap(value => this.apiservice.get<UserModel[]>(`api/v1/users/all?name=${value}`)
        )
      ).subscribe(res => this.users = res)
    )
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

  openFormModal(userId: number) {
    const initialState = {
      id: userId
    }

    this.bsModalRef = this.bsModalService.show(FormUserComponent, { initialState: initialState })
  }

  openDeleteModal(userId: number){
    const initialState = {
      id: userId
    }

    this.bsModalRef = this.bsModalService.show(DeleteuserComponent, { initialState: initialState })
  }

  ngOnDestroy(): void {
    this.sublist.forEach(sub => sub.unsubscribe());
  }
}
