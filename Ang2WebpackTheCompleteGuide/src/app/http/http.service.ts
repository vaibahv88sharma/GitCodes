import { Injectable } from '@angular/core';
import { Http, Headers, Response } from "@angular/http";
import 'rxjs/Rx';
import { Observable } from "rxjs/Rx";

@Injectable()
export class HttpService {

    private hostweburl: string = decodeURIComponent(this.manageQueryStringParameter("SPHostUrl"));
    private appweburl: string = decodeURIComponent(this.manageQueryStringParameter("SPAppWebUrl"));	
    private spApiUrl: string;
    private spListName: string = 'Demo';


    constructor(private http: Http) {

        console.log(this.hostweburl);
        console.log(this.appweburl);
        this.spApiUrl = this.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Demo')/items?" +
            "@target='" + this.hostweburl + "'";	

    }

    getData() {
        return this.http.get(this.spApiUrl, { headers: this.getHeaders() })
            .map((response: Response) => response.json());
    }

    getOwnData() {
        return this.http.get(this.spApiUrl, { headers: this.getHeaders() })
            .map((response: Response) => response.json());
    }

    sendData(username: string, email: string) {
        var obj = {
            '__metadata': { 'type': "SP.Data." + this.spListName + "ListItem" },
            'Title': 'Test',
            'username': username,
            'email': email
        };
        var data = JSON.stringify(obj);

        return this.http.post(this.spApiUrl, data, { headers: this.getHeaders("POST") })
            .map((data: Response) => data.json())
            .catch(this.handleError);
    }

    private handleError(error: any) {
        console.log(error);
        return Observable.throw(error.json());
    }

    manageQueryStringParameter(paramToRetrieve: any) {
        var params =
            document.URL.split("?")[1].split("&");
        //var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    }

    // GET HEADERS, esta función resuelve las cabeceras segun el verbo que estemos utilizando
    private getHeaders(verb?: string) {
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

}
