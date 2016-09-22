import {UserRegisterModel} from '../models/userModels';
import {autoinject} from 'aurelia-framework';
import {UserRegisterService} from '../services/registerService';

@autoinject()
export class RegisterUserViewModel
{
    userRegisterModel: UserRegisterModel;

    constructor(private registerService: UserRegisterService) {
        this.userRegisterModel = new UserRegisterModel();    
        this.registerService.test()    
    }
}