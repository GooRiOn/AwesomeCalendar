import {UserRegisterModel} from '../models/userModels';
import {autoinject} from 'aurelia-framework';
import {UserRegisterService} from '../services/registerService';
import * as toastr from "toastr";

@autoinject()
export class UserRegisterViewModel
{
    userRegisterModel: UserRegisterModel;   

    constructor(private registerService: UserRegisterService) 
    {
        this.userRegisterModel = new UserRegisterModel();            
    }

    activate()
    {
        toastr.success('tets')
    }

    register() 
    {
        this.registerService.register(this.userRegisterModel).then(() =>
        {
            toastr.success('User registered successfuly');
        });
    }
}