import { Component, forwardRef, Input } from '@angular/core';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ErroMsgComponent } from '../erro-msg/erro-msg.component';

const INPUT_FIELD_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => InputFieldComponent),
  multi: true
};

@Component({
  selector: 'app-input-field',
  standalone: true,
  imports: [FormsModule, ErroMsgComponent],
  templateUrl: './input-field.component.html',
  styleUrl: './input-field.component.css',
  providers: [INPUT_FIELD_VALUE_ACCESSOR]
})
export class InputFieldComponent {
  @Input() id: string;
  @Input() label: string;
  @Input() type: string = "text";
  @Input() control: any;
  @Input() isReadOnly = false;

  private InnerValue: any;

  get value(){
    return this.InnerValue;
  }
  set value(v: any){
    if (v !== this.InnerValue){
      this.InnerValue = v;
      this.onChangeCallBack(v);
    }
  }

  constructor(){}

  onChangeCallBack: (_: any) => void = () => {};
  onTouchedCallBack: (_: any) => void = () => {};

  writeValue(obj: any): void{
    this.value = obj
  }

  registerOnChange(fn: any): void{
    this.onChangeCallBack = fn;
  }

  registerOnTouched(fn: any): void{
    this.onTouchedCallBack = fn;
  }

  setDesableState(isDesable: boolean): void{
    this.isReadOnly = isDesable;
  }
}
