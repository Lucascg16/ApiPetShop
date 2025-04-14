import { Component, Input, OnDestroy } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form.component';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from '../../../Model/enum/shopEnum.enum';
import { ErroMsgComponent } from '../../../Shared/erro-msg/erro-msg.component';

@Component({
  selector: 'app-petform',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent, ErroMsgComponent, FormsModule],
  templateUrl: './petform.component.html',
  styleUrl: './petform.component.css'
})
export class PetformComponent  extends BaseFormComponent implements OnDestroy{
  @Input() date: Date;
  disableEmail:boolean = true;//a tela abre  com o telefone selecionado.
  disablePhone:boolean = false;

  type = PetTypeEnum;
  petGender = PetGenderEnum;
  petsize = PetSizeEnum;
  schedulerTimes = "";

  constructor(private http: HttpClient, private formbuilder: FormBuilder){
    super();

    this.form = formbuilder.group({
      name: [null, [Validators.required]],
      contactMethod: [1],
      email: [null, [Validators.email]],
      phone: [null], //desenvolver o validador de telefone
      isWpp: [false],
      petName: [null, Validators.required],
      petAge: [null, Validators.required],
      type: [null, Validators.required],
      petGender: [null, Validators.required],
      petSize: [null, Validators.required],
      scheduler: [null, Validators.required]     
    });
  }

  verifyContact(){
    console.log(this.form.get('contactMethod')?.value);
    this.disableEmail = false;
    this.disablePhone = false;

    if(this.form.get('contactMethod')?.value === 1){
      this.disableEmail = true;
    }
    if(this.form.get('contactMethod')?.value === 2){
      this.disablePhone = true;
    }

  }
  
  override submit() {

  }

  ngOnDestroy(): void {

  }
}
