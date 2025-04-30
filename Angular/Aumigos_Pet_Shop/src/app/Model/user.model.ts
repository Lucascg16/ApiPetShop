import { UserEnum } from "./enum/shopEnum.enum";

export interface UserModel{
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    role: UserEnum
}