import { Component, OnInit } from '@angular/core';
import { IBaseModal } from '../../../Shared/base-form/base-modal-Interface';
import { ApiServices } from '../../../Services/petShopApi.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UserModel } from '../../../Model/user.model';
import { sessionModel } from '../../../Model/sessionModel.model';

@Component({
  selector: 'app-deleteuser',
  imports: [],
  templateUrl: './deleteuser.component.html',
  styleUrl: './deleteuser.component.css'
})
export class DeleteuserComponent implements OnInit, IBaseModal {
  id: number;
  alertmsg: any;
  sending: boolean = false;
  currentuser: sessionModel;

  constructor(private bsModalRef: BsModalRef, private apiservice: ApiServices) { }

  ngOnInit(): void {
    this.currentuser = JSON.parse(sessionStorage.getItem('currentUser') as string)
  }

  async deleteUser() {
    this.sending = true;
    if(this.currentuser.id === this.id){
      this.alertmsg = { message: "Não é possivel remover você mesmo", isSuccesse: false };
      this.sending = false;
      return;
    }
    
    try {
      await this.apiservice.delete("api/v1/users")

      this.alertmsg = { message: "Usuário removido com sucesso", isSuccesse: true };
      this.sending = false
    }
    catch (err) {
      this.alertmsg = { message: "Ocorreu algum erro ao remover o usuário, tente novamente mais tarde ou contate um administrador", isSuccesse: false };
      this.sending = false;
    }
  }

  closeModal(): void {
    this.bsModalRef.hide();
  }
}
