import { Component, OnInit, DoCheck, OnDestroy } from '@angular/core';


@Component({
  selector: 'first-feature',
  templateUrl: './first-feature.component.html',
  styleUrls: ['./first-feature.component.css']
})
export class FirstFeatureComponent implements OnInit, DoCheck, OnDestroy { 

  page = { title:"My first feature component", description:"A description"}
  oldTitle= "";
  childInputValue= "childInputValue";

  ngOnInit(){
    console.log('First Feature component initialised. ');
  }

  ngDoCheck(){
    console.log('First Feature component ngDoCheck called. ');

    if(this.oldTitle !== this.page.title){
    console.log('First Feature component was changed. ');      
      this.oldTitle = this.page.title;
    }
  }

  ngOnDestroy(){
    console.log('First Feature component destroyed. ');
  }

}
