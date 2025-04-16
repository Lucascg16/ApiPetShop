import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from "./enum/shopEnum.enum";

export interface PetserviceModel {
    id: number;
    name: string;
    email?: string;
    phoneNumber?: string;
    isWhatApp: boolean;
    petName: string;
    petAge: number;
    type: PetTypeEnum;
    petGender: PetGenderEnum;
    petSize: PetSizeEnum;
    scheduledDate: string;
}