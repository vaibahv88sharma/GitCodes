import { Component, OnInit } from '@angular/core';
import { Response } from "@angular/http";
import { HttpService } from "./http.service";

@Component({
  selector: 'app-http',
  templateUrl: './http.component.html',
  styleUrls: ['./http.component.css']
})
export class HttpComponent implements OnInit {

    items: any[] = [];
    asyncString = this.httpService.getData();
    constructor(private httpService: HttpService) {

    }

    ngOnInit() {
        this.httpService.getData()
            .subscribe(
            (data: any) => console.log(data));
  }

    onSubmit(username: string, email: string) {
        //this.httpService.sendData({ username: username, email: email })
        this.httpService.sendData( username, email)
            .subscribe(
            (data: any) => console.log(data),
            (error: any) => console.log(error)
            );
    }

    onGetData() {
        this.httpService.getOwnData()
            .subscribe(
            (data: any) => {
                const myArray = [];
                for (var i = 0; i < data.d.results.length; i++) {
                    //for (let key in data) {
                    myArray.push(data.d.results[i]);
                }
                this.items = myArray;
            }
            );
    }

}
