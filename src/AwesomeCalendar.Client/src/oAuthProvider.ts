export interface IOAuthProvider
{
    getAccessToken() : string;
    setAccessToken(token: string, isLongTermStored: boolean) : void;    
}

export class OAuthProvider implements IOAuthProvider
{
    private readonly storageKey = 'access_token';
    private storage;

    getAccessToken()
    {
        return this.storage.getElementById(this.storageKey); 
    }

    setAccessToken(token: string, isLongTermStored: boolean)
    {
        this.storage = isLongTermStored? localStorage : sessionStorage;
        this.storage.setItem(this.storageKey, token);
    }
}