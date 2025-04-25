import { PetGenderEnum, PetSizeEnum, PetTypeEnum } from "./enum/shopEnum.enum";
import { vacineModel } from "./vacine.model";

export interface VetServiceModel{
    id: number;
    name: string;
    email?: string;
    phoneNumber?: string;
    isWhatsApp: boolean;
    petName: string;
    petAge: string;
    petType: PetTypeEnum;
    petGender: PetGenderEnum;
    petSize: PetSizeEnum;
    scheduledDate: Date;
    petWeight: number;
    isCastrated: boolean;
    vacines: vacineModel[];
}