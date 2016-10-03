import {HttpClient, json} from 'aurelia-fetch-client';
import { IOAuthProvider } from '../src/oAuthProvider';

export abstract class DataService
{    
    constructor(private httpClient: HttpClient, private oAuthProvider: IOAuthProvider)
    {        
        this.httpClient.configure(config => {
            config.withBaseUrl('http://localhost:4000');            
        })
    }

    protected get(url: string, isAuthorized: boolean) 
    {
        return this.createHttpRequest('get', url, null, isAuthorized);
    }

    protected post<TData, TResponse>(url: string, data: TData, isAuthorized: boolean)
    {
        return this.createHttpRequest('post', url, data, isAuthorized);
    }

    protected put<TData, TResponse>(url: string, data: TData, isAuthorized: boolean)
    {
        return this.createHttpRequest('put', url, data, isAuthorized);
    }

    protected delete<TData, TResponse>(url: string, data: TData, isAuthorized: boolean)
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

