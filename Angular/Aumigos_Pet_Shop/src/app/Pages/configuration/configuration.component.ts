import { Component } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UpdatepasswordComponent } from '../passwordPages/updatepassword/updatepassword.component';
import { CompanieComponent } from './companie/companie.component';

@Component({
  selector: 'app-configuration',
  imports: [],
  templateUrl: './configuration.component.html',
  styleUrl: './configuration.component.css'
})
export class ConfigurationComponent {
  bsModelRef: BsModalRef;

  constructor(private bsModelService: BsModalService){

  }

  openPassModal(){
    this.bsModelRef = this.bsModelService.show(UpdatepasswordComponent);
  }

  openEnterpriseModal(){
    this.bsModelRef = this.bsModelService.show(CompanieComponent)
  }

  openMessageModal(){

  }
}
