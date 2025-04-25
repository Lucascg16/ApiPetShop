import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom, Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class FormServices {
    constructor(private http: HttpClient) { }

    getAvailableTimes(url: string) {
        return this.http.get<string[]>(url);
    }

    getAnyData<T>(url: string): Observable<T> {
        return this.http.get<T>(url);
    }

    async createOrUpdateService(url: string, modelBody: any){
        if (modelBody.id === 0) {
            return await firstValueFrom(this.http.post(url, modelBody));
        } else {
            return await firstValueFrom( this.http.patch(url, modelBody));
        }
    }
}