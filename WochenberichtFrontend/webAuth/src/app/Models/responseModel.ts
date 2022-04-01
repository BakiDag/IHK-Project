import { responseCode } from "../enums/responseCode";



export class responseModel
{
    public responseCode : responseCode=responseCode.NotSet;
    public responseMessage:string ="";
    public dateSet: any;
}