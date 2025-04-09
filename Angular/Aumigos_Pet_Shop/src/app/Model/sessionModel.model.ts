export class sessionModel{
    id: number;
    role: string;
    token: string;
    refreshToken: string;
    refreshKey:string;

    constructor(id: number, role: string, token: string, refreshToken:string, refreshKey:string){
        this.id = id;
        this.role = role;
        this.token = token; 
        this.refreshToken = refreshToken;
        this.refreshKey = refreshKey;
    }
}