import {HttpClient, json} from 'aurelia-fetch-client';
import { IOAuthProvider } from '../src/oAuthProvider';

export interface IDataService
{
    get<TResponse>(url: string, isAuthorized: boolean) : Promise<TResponse>;
    post<TData, TResponse>(url: string, data: TData, isAuthorized: boolean) : Promise<TResponse>;
    put<TData, TResponse>(url: string, data: TData, isAuthorized: boolean) : Promise<TResponse>;
    delete<TData, TResponse>(url: string, data: TData, isAuthorized: boolean) : Promise<TResponse>;
}

export abstract class DataService implements IDataService
{    
    constructor(private httpClient: HttpClient, private oAuthProvider: IOAuthProvider)
    {        
        this.httpClient.configure(config => {
            config.withBaseUrl('http://localhost:4000');            
        })
    }

    get(url: string, isAuthorized: boolean) 
    {
        return this.createHttpRequest('get', url, null, isAuthorized);
    }

    post<TData, TResponse>(url: string, data: TData, isAuthorized: boolean)
    {
        return this.createHttpRequest('post', url, data, isAuthorized);
    }

    put<TData, TResponse>(url: string, data: TData, isAuthorized: boolean)
    {
        return this.createHttpRequest('put', url, data, isAuthorized);
    }

    delete<TData, TResponse>(url: string, data: TData, isAuthorized: boolean)
    {
        return this.createHttpRequest('delete', url, data, isAuthorized);
    }

    private createHttpRequest<TData, TResponse>(method: string, url: string, data: TData, isAuthorized: boolean) : Promise<TResponse>
    {
        let requestConfig : any = 
        {
            method: method,
            body: data
        };
        
        if(isAuthorized)
        {
            let accessToken = this.oAuthProvider.getAccessToken();
            requestConfig.header = {'Authorization' : `Bearer ${accessToken}`};
        }          

        return this.httpClient.fetch(url, requestConfig).then<TResponse>(response => response.json());
    }
}

