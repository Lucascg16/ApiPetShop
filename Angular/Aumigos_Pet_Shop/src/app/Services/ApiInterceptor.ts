import { HttpInterceptorFn } from "@angular/common/http";

const Base_Url =  "https://localhost:7030";

export const ApiInterceptor: HttpInterceptorFn = (req, next) => {
    let currentUser = JSON.parse(sessionStorage.getItem('currentUser') ?? "");
        
    req = req.clone({ url: `${Base_Url}/${req.url}` })
    
    if(currentUser && currentUser.token){
        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${currentUser.token}`
            }
        } )
    }

    return next(req);
}