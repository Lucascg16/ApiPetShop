import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { sessionModel } from "../Model/sessionModel";
import { environment } from "../../environments/environment.development";
import { ITokenService } from "./interface/ITokenService";
import { TokenService } from "./token-service.service";

const Base_Url = environment.api_url;

export const ApiInterceptor: HttpInterceptorFn = (req, next) => {    
    const tokenService: ITokenService = inject(TokenService);
    let userdata;

    req = req.clone({ url: `${Base_Url}/${req.url}` })

    try{
        userdata = sessionStorage.getItem('currentUser');
    }catch{}

    if(userdata){
        tokenService.refreshToken();

        try {
            const currentUser: sessionModel = JSON.parse(userdata)
            if (currentUser.token) {
                req = req.clone({
                    setHeaders: {
                        Authorization: `Bearer ${currentUser.token}`
                    }
                })
            }
        }
        catch { }
    }
    return next(req);
}