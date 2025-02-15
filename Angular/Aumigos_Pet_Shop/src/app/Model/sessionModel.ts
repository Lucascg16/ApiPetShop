export class sessionModel{
    id: number;
    role: string;
    token: string;

    constructor(id: number, role: string, token: string){
        this.id = id;
        this.role = role;
        this.token = token; 
    }
}