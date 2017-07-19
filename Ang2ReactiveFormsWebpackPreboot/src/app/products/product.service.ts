import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';

import { IProduct } from './product';
import { SharePointApp } from '../shared/sharepoint';
//import {SpAppSettings} from '../shared/spAppSettings';

@Injectable()
export class ProductService {
    private baseUrl = 'api/products';
    //private baseUrl = './products.json';

    //SharePoint
    private sp: SharePointApp;

    constructor(private http: Http) { 
        this.sp = new SharePointApp();
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
       
    getProducts(): Observable<IProduct[]> {
        return this.http.get(
                this.sp.getURL('products'),//+"&$select=Id,productId,productName,productCode,releaseDate,description,price,starRating,imageUrl,tags", 
                { headers: this.getHeaders() }
            )
            .map(this.extractData)
            .do(data => console.log('getProducts: ' + JSON.stringify(data)))
            .catch(this.sp.handleError);
    }
/*    getProducts(): Observable<IProduct[]> {
        return this.http.get(this.baseUrl)
            .map(this.extractData)
            .do(data => console.log('getProducts: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }*/

    getProduct(id: number): Observable<IProduct> {
        if (id === 0) {
        return Observable.of(this.initializeProduct());
        // return Observable.create((observer: any) => {
        //     observer.next(this.initializeProduct());
        //     observer.complete();
        // });
        };
        const url = this.sp.getURL('products');
        return this.http.get(
                url,
                { headers: this.getHeaders() }
            )
            .map(this.extractData)
            .do(data => console.log('getProduct: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
/*    getProduct(id: number): Observable<IProduct> {
        if (id === 0) {
        return Observable.of(this.initializeProduct());
        // return Observable.create((observer: any) => {
        //     observer.next(this.initializeProduct());
        //     observer.complete();
        // });
        };
        const url = `${this.baseUrl}/${id}`;
        return this.http.get(url)
            .map(this.extractData)
            .do(data => console.log('getProduct: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }*/    

    deleteProduct(id: number): Observable<Response> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        const url = `${this.baseUrl}/${id}`;
        return this.http.delete(url, options)
            .do(data => console.log('deleteProduct: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    saveProduct(product: IProduct): Observable<IProduct> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        if (product.id === 0) {
            return this.createProduct(product, options);
        }
        return this.updateProduct(product, options);
    }

    private createProduct(product: IProduct, options: RequestOptions): Observable<IProduct> {
        product.id = undefined;
        return this.http.post(this.baseUrl, product, options)
            .map(this.extractData)
            .do(data => console.log('createProduct: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private updateProduct(product: IProduct, options: RequestOptions): Observable<IProduct> {
        const url = `${this.baseUrl}/${product.id}`;
        return this.http.put(url, product, options)
            .map(() => product)
            .do(data => console.log('updateProduct: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private extractData(response: Response) {
        let body = response.json();
        let products: IProduct[] =[];

        for(let i in body.d.results){
            products.push(
                {
                    'id': body.d.results[i]["Id"],
                    'productName': body.d.results[i]["productName"],
                    'productCode': body.d.results[i]["productCode"],
                    'releaseDate': body.d.results[i]["releaseDate"],
                    'description': body.d.results[i]["description"],
                    'price': body.d.results[i]["price"],
                    'starRating': body.d.results[i]["starRating"],
                    'imageUrl': body.d.results[i]["imageUrl"],
                    'tags': ['']
                }              
            );
        }

        //return body.d.results || {};
        return products || {};
    }

    private handleError(error: Response): Observable<any> {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }

    initializeProduct(): IProduct {
        // Return an initialized object
        return {
            id: 0,
            productName: null,
            productCode: null,
            tags: [''],
            releaseDate: null,
            price: null,
            description: null,
            starRating: null,
            imageUrl: null
        };
    }
}
