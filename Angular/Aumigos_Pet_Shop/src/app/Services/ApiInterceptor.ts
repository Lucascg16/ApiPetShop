import { HttpInterceptorFn } from "@angular/common/http";
import { sessionModel } from "../Model/sessionModel";

const Base_Url = "https://localhost:7030";

export const ApiInterceptor: HttpInterceptorFn = (req, next) => {
    req = req.clone({ url: `${Base_Url}/${req.url}` })
    
    let userdata = sessionStorage.getItem('currentUser');
    if(userdata){
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