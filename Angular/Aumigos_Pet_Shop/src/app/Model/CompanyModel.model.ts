import { AddressModel } from "./AddressModel.model";

export interface CompanyModel {
    name: string;
    contactEmail: string;
    address: AddressModel;
    phoneNumber: Number;
    instagramAddress: string;    
}