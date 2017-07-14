import { Component, OnDestroy, OnInit, ElementRef, AfterContentInit, ContentChild, AfterViewChecked, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from "rxjs/Rx";

import { IData } from '../shared/data';
import { DataService } from '../shared/data.service';
import {SpAppSettings} from '../shared/sp-app-settings';

@Component({
  selector: 'app-hyperlink',
  templateUrl: './hyperlink.component.html',
  styleUrls: ['./hyperlink.component.css']
})
export class HyperlinkComponent implements OnInit, OnDestroy, AfterContentInit, AfterViewChecked {

    data: IData[];
    errorMessage: string;
    private subscription: Subscription;
    private listName: string;
    private appTitle: string;    
    private iframeHeight: number = 10;

  @ContentChild('boundContent')
  boundContent: HTMLElement;

   @ViewChild('eMainFrame') eMainFrame : ElementRef;

    constructor(private ds: DataService, private elementRef:ElementRef) {
        //console.log(this.elementRef.nativeElement.offsetHeight);
    }

    ngOnInit(): void {
        //console.log("ngOnInit:-  " + this.boundContent);
        this.listName= SpAppSettings.APP_LIST;//"hyperlinks01";
        this.appTitle= SpAppSettings.APP_HEADTITLE;//"DEVELOPING GREAT PEOPLE AND PLACES";        
        this.ds.getData()
                .subscribe(data => this.data = data,
                           error => this.errorMessage = <any>error);
    }
onResize(event: any) {
  console.log("onResize:- "+event.target.innerHeight); 
  //console.log(this.elementRef.nativeElement.offsetHeight);
}

  ngAfterContentInit() {
    //console.log('4 ngAfterContentInit');
    //console.log("ngAfterContentInit:-  " + this.boundContent);
  }

  ngAfterViewChecked() {
    console.log("child ngAfterViewChecked eMainFrame:-  "+this.eMainFrame.nativeElement.offsetHeight);  
    //console.log("window.document.URL:- "+window.document.URL);  

    if (this.iframeHeight != this.eMainFrame.nativeElement.offsetHeight){
      this.iframeHeight = this.eMainFrame.nativeElement.offsetHeight;
      console.log("changed iframeHeight:-  "+this.eMainFrame.nativeElement.offsetHeight);
      let id = this.getQsParam("SenderId");
      let message = "<message senderId=" + id + ">resize(100%," + this.iframeHeight + ")</message>";
      window.parent.postMessage(message, "*");
    }
  }

  private getQsParam(name: any) {
      name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
      var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
      results = regex.exec(location.search);
      return results === null ? "" : results[1].replace(/\+/g, " ");
  }

  ngOnDestroy() {
      //this.subscription.unsubscribe();
  }

}
