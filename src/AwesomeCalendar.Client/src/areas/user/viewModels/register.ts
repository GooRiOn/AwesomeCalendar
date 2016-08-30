import {UserRegisterModel} from '../models/userModels';

export class RegisterUserViewModel
{
    userRegisterModel: UserRegisterModel;

    constructor() {
        this.userRegisterModel = new UserRegisterModel();        
    }
}