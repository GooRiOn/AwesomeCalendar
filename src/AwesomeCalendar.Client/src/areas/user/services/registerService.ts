import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {IOAuthProvider} from '../../../oAuthProvider';
import {DataService} from '../../../dataService';
import {UserRegisterModel} from '../models/userModels';

export interface IUserRegisterService
{
    register(model: UserRegisterModel) : Promise<any>;    
} 

@autoinject()
export class UserRegisterService extends DataService implements IUserRegisterService
{
    constructor(httpClient: HttpClient, oAuthProvider: IOAuthProvider) {
        super(httpClient, oAuthProvider);
    }

    register(model: UserRegisterModel)
    {
        return super.post('user/register', model, false);
    }
}