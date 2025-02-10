import { Component, Input } from '@angular/core';
import { FormValidator } from '../base-form/form-validator';

@Component({
  selector: 'app-erro-msg',
  standalone: true,
  imports: [],
  templateUrl: './erro-msg.component.html',
  styleUrl: './erro-msg.component.css'
})
export class ErroMsgComponent {
  @Input() control: any;
  @Input() label: string;

  constructor(){}

  get errorMessage(){
    for(let propName in this.control.errors){
      if(this.control.errors.hasOwnProperty(propName) && this.control.touched){
        return FormValidator.getErrorMsg(this.label, propName, this.control.errors[propName]);
      }
    }
  }
}
