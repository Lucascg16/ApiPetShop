import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ApiServices {
    constructor(private http: HttpClient) { }

    get<T>(url: string): Observable<T> {
        return this.http.get<T>(url);
    }

    async post(url: string, modelBody: any){
        return await firstValueFrom(this.http.post(url, modelBody));
    }

    async patch(url: string, modelBody: any){
        return await firstValueFrom(this.http.patch(url, modelBody));
    }

    async delete(url: string){
        return await firstValueFrom(this.http.delete(url));
    }
}