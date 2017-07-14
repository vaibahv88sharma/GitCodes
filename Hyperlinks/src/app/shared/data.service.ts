import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { IData } from './data';
import { SharePointApp } from '../shared/sharepoint';
import {SpAppSettings} from '../shared/sp-app-settings';

@Injectable()
export class DataService {

    private baseUrl = './hyperlinks.json';

    getData(): Observable<IData[]> {
        return this.http.get(
                this.sp.getURL(SpAppSettings.APP_LIST),//('Hyperlink01'),//+"&$select=Id,productId,productName,productCode,releaseDate,description,price,starRating,imageUrl,tags", 
                { headers: this.getHeaders() }
            )
            .map(this.extractData)
            //.do((data: any) => console.log('getProducts: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

/*    getData(): Observable<IData[]> {
        return this.http.get(
                this.baseUrl
            )
            .map(this.extractData)
            .do((data: any) => console.log('getProducts: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }*/

    private extractData(response: Response) {
        let body = response.json();
        let data: IData[] =[];

/*        for(let i in body){
            data.push(
                {
                    'hyperlink': body[i]["hyperlink"],
                    'hyperlinkTitle': body[i]["hyperlinkTitle"],
                    'hyperlinkTarget': body[i]["hyperlinkTarget"],
                }              
            );
        }*/
        for(let i in body.d.results){
            data.push(
                {
                    'hyperlink': body.d.results[i]["hyperlink"],
                    'hyperlinkTitle': body.d.results[i]["hyperlinkTitle"],
                    'hyperlinkTarget': body.d.results[i]["hyperlinkTarget"],
                }              
            );
        }
        return data || {};
    }

/*    private handleError(error: Response): Observable<any> {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }*/
    private handleError(error: any) {
        console.log(error);
        return Observable.throw(error.json());
    }

    constructor(private http: Http, private sp: SharePointApp) {
    //constructor(private http: Http) {
        //this.sp = new SharePointApp();
    }

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

}
