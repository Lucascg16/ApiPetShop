import { AddressModel } from "./AddressModel.model";

export interface CompanyModel {
    Name: string;
    contactEmail: string;
    address: AddressModel;
    phoneNumber: Number;
    instagramAddress: string;    
}