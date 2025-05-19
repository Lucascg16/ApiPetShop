import { AddressModel } from "./AddressModel.model";

export interface CompanyModel {
    id: number;
    name: string;
    contactEmail: string;
    address: AddressModel;
    phoneNumber: Number;
    instagramAddress: string;    
}