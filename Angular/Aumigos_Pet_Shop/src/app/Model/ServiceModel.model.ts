import { PetTypeEnum } from "./enum/typeEnum.enum";

export interface ServiceModel{
    id:Number;
    name:string;
    email: string;
    petName:string;
    phoneNumber:string;
    scheduledDate: Date;
    type: PetTypeEnum;
}