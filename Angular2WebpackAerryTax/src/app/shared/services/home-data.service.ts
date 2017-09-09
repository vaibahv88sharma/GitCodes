import { Injectable } from '@angular/core';
import { Http, Headers, Response } from "@angular/http";
import { Observable } from "rxjs/Rx";

@Injectable()
export class HomeDataService {

    constructor(private http: Http) {
    }

    getTaxation(url: string) : Observable<any> {
        return this.http.get(
                url                
            )
            //.delay(5000)
            .map(this.handleSuccess)
            .catch(this.handleError)
    }

    getData(url: string) : Observable<any> {
        return this.http.get(
                url                
            )
            //.delay(5000)
            .map(this.handleSuccess)
            .catch(this.handleError)
    }

    private handleSuccess(res: Response) {
      //debugger;
        let body = res.json();
        //console.log(body);

        //let products: IProduct[] =[];
        //for(let i in body.d.results){
        //    products.push(
        //        {
        //            'id': body.d.results[i]["Id"],
        //            'productName': body.d.results[i]["productName"],
        //            'productCode': body.d.results[i]["productCode"],
        //            'releaseDate': body.d.results[i]["releaseDate"],
        //            'description': body.d.results[i]["description"],
        //            'price': body.d.results[i]["price"],
        //            'starRating': body.d.results[i]["starRating"],
        //            'imageUrl': body.d.results[i]["imageUrl"],
        //            'tags': ['']
        //        }              
        //    );
        //}
        //return products || {};

        return body || {};
    }

    private handleError(error: any) {
        debugger;
        console.log(error);
        return Observable.throw(error.json());
    }

}
