import { PetTypeEnum } from "./enum/shopEnum.enum";

export interface ServiceModel{
    id:Number;
    name:string;
    email: string;
    petName:string;
    phoneNumber:string;
    scheduledDate: Date;
    type: PetTypeEnum;
}