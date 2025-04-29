import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from "./enum/shopEnum.enum";

export interface PetserviceModel {
    id: number;
    name: string;
    email?: string;
    phoneNumber?: string;
    isWhatsApp: boolean;
    petName: string;
    petAge: number;
    petType: PetTypeEnum;
    petGender: PetGenderEnum;
    petSize: PetSizeEnum;
    scheduledDate: string;
}