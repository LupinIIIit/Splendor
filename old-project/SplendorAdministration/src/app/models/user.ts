export class User {
    sub:string;
    name:string;
    fullname:string;
    email:string;
    jobtitle:string;
    phone:string;
    isEnabled:boolean;
    Roles:string[];
    constructor(sub:string,
        name:string,
        fullname:string,
        email:string,
        jobtitle:string,
        phone:string,
        isEnabled:boolean,
        Roles:string[]){

        }
    constructor(username:string,password:string,other:String){
        
    }
}