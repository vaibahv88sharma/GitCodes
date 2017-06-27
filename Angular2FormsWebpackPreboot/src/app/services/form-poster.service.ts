import { Injectable } from '@angular/core';
import { Http, Headers, Response } from "@angular/http";
import { Observable } from "rxjs/Rx";


//import { REST } from "../shared/REST";

@Injectable()
export class FormPosterService {



    // REST
    private hostweburl: string = decodeURIComponent(this.manageQueryStringParameter("SPHostUrl"));
    private appweburl: string = decodeURIComponent(this.manageQueryStringParameter("SPAppWebUrl"));
    public spApiUrl: string;
    //private spListName: string = 'recipe';


    constructor(private http: Http) {

    }

//postEmployees
    postEmployees(body: string, listName: string) {

        //const headers = this.getHeaders();
        return this.http.post(
            this.getURL(listName),
                body,
                { headers: this.getHeaders("POST") })
            //.map((response: Response) => response.json())
            .map(this.handleData)
            .catch(this.handleError);
    }

    getLanguages(listName: string, q: string) : Observable<any> {
        return this.http.get(
                this.getURL(listName) + q,
                { headers: this.getHeaders() }
            )
            .delay(5000)
            .map(this.handleLanguagesData)
            .catch(this.handleError)
    }

    // REST Error Handling
    private handleError(error: any) {
        console.log(error);
        return Observable.throw(error.json());
    }
    // REST Data Handling
    private handleData(res: Response) {
        let body = res.json();
        console.log(body.d);
        return body || {};
    }
    // REST Filter Data
    private handleLanguagesData(res: Response) {
        let body = res.json();
        console.log(body.d);
        var bodyFilter: string[] = [];
        for(let i in body.d.results){
            console.log(body.d.results[i]["primaryLanguage"]);
            bodyFilter[i]= body.d.results[i]["primaryLanguage"];
        }

        return bodyFilter || {};
    }

    // List URL
    public getURL(listName: string) {
        return this.spApiUrl = this.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listName + "')/items?" +
            "@target='" + this.hostweburl + "'";
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
}
