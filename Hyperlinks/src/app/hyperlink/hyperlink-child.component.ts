import { Component, 
        OnDestroy, 
        OnInit, 
        AfterContentInit, 
        ContentChild,
        ViewChild,
        AfterViewChecked,
        ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from "rxjs/Rx";

import { IData } from '../shared/data';
import { DataService } from '../shared/data.service';

@Component({
  selector: 'app-hyperlink-child',
  templateUrl: './hyperlink-child.component.html',
  styleUrls: ['./hyperlink-child.component.css']
})
export class HyperlinkChildComponent implements OnInit, OnDestroy, 
      AfterContentInit, AfterViewChecked {

    data: IData[];
    errorMessage: string;
    //private subscription: Subscription;
    private listName: string;
    private appTitle: string;    

  @ContentChild('boundContent')
  boundContent: HTMLElement;


   @ViewChild('eMainFrame') eMainFrame : ElementRef;

    constructor(private ds: DataService) {
    }

    ngOnInit(): void {

        this.listName= "hyperlinks01";
        this.appTitle= "DEVELOPING GREAT PEOPLE AND PLACES";        

        this.ds.getData()
                .subscribe(data => this.data = data,
                           error => this.errorMessage = <any>error);
        //console.log("child ngOnInit eMainFrame:-  "+this.eMainFrame.nativeElement.offsetHeight);
    }

  ngDoCheck() {
    //onsole.log(this.eMainFrame.nativeElement.offsetWidth);
    //console.log("child ngDoCheck eMainFrame:-  "+this.eMainFrame.nativeElement.offsetHeight); 
  }

  ngAfterContentInit() {
    //console.log('4 ngAfterContentInit');
    //console.log("ngAfterContentInit:-  " + this.boundContent);
    //console.log("child ngAfterContentInit eMainFrame:-  "+this.eMainFrame.nativeElement.offsetHeight);  
  }

  ngAfterViewChecked() {
    console.log("child ngAfterViewChecked eMainFrame:-  "+this.eMainFrame.nativeElement.offsetHeight);  
    //console.log("window.document.URL:- "+window.document.URL);  
  }

  ngOnDestroy() {
      //this.subscription.unsubscribe();
  }

}
