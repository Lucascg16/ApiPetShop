import { Component, Input, OnDestroy } from '@angular/core';
import { BaseFormComponent } from '../../../Shared/base-form/base-form.component';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputFieldComponent } from '../../../Shared/input-field/input-field.component';
import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from '../../../Model/enum/shopEnum.enum';

@Component({
  selector: 'app-petform',
  standalone: true,
  imports: [ReactiveFormsModule, InputFieldComponent],
  templateUrl: './petform.component.html',
  styleUrl: './petform.component.css'
})
export class PetformComponent  extends BaseFormComponent implements OnDestroy{
  @Input() date: Date;

  type = PetTypeEnum;
  petGender = PetGenderEnum;
  petsize = PetSizeEnum;

  constructor(private http: HttpClient, private formbuilder: FormBuilder){
    super();

    this.form = formbuilder.group({
      name: [null, [Validators.required]],
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
  
  override submit() {
    throw new Error('Method not implemented.');
  }

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }
}
