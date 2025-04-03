import { AddressModel } from "./AddressModel";

export interface CompanyModel {
    Name: string;
    contactEmail: string;
    address: AddressModel;
    phoneNumber: Number;
    instagramAddress: string;    
}