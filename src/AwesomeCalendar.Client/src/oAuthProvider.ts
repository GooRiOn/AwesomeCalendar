export interface IOAuthProvider
{
    getAccessToken() : string;
    setAccessToken(token: string, isLongTermStored: boolean) : void;    
}

export class OAuthProvider implements IOAuthProvider
{
    getAccessToken()
    {
        return null;
    }

    setAccessToken(token: string, isLongTermStored: boolean)
    {

    }
}