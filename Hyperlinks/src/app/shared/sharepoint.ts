import { Observable } from "rxjs/Rx";
import { Injectable } from '@angular/core';
import {SpAppSettings} from './sp-app-settings';

@Injectable()
export class SharePointApp {


    public spApiUrl: string;
    public listUrl: string;
    public appContextUrl: string;

    constructor() {
    }

    init(){
        SpAppSettings.hostweburl = decodeURIComponent(this.manageQueryStringParameter("SPHostUrl"));
        SpAppSettings.appweburl = decodeURIComponent(this.manageQueryStringParameter("SPAppWebUrl"));      

        SpAppSettings.APP_HEADTITLE = decodeURIComponent(this.getQueryStringParameter("AppTitle"));
        SpAppSettings.APP_LIST = decodeURIComponent(this.getQueryStringParameter("ListName"));        
    }

    // REST Error Handling
    public handleError(error: any) {
        console.log(error);
        return Observable.throw(error.json());
    }

    // List URL
    public getURL(listName: string) {
        return this.spApiUrl = SpAppSettings.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listName + "')/items?" +
            "@target='" + SpAppSettings.hostweburl + "'";        
    }

    //REST GET HEADERS, this function resolves the headers according to the verb that we are using
    public getHeaders(verb?: string) {
        var headers = new Headers();
        //var digest = document.getElementById('__REQUESTDIGEST').value;
        var digest = (<HTMLInputElement>document.getElementById('__REQUESTDIGEST')).value;

        headers.set('X-RequestDigest', digest);
        headers.set('Accept', 'application/json;odata=verbose');

        switch (verb) {
            case "POST":
                headers.set('Content-type', 'application/json;odata=verbose');
                break;
            case "PUT":
                headers.set('Content-type', 'application/json;odata=verbose');
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "MERGE");
                break;
            case "DELETE":
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "DELETE");
                break;
        }
        return headers;
    }


    // REST URL
    public manageQueryStringParameter(paramToRetrieve: any) {
        var params =
            document.URL.split("?")[1].split("&");
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    }
    // REST SP.Data.List
    public getItemTypeForListName(name: string) {
        return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
    }


    private getQueryStringParameter(paramToRetrieve: string) {
        var params;
        var strParams;

        params = document.URL.split("?")[1].split("&");
        strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve)
                return singleParam[1];
        }
    }    

}
