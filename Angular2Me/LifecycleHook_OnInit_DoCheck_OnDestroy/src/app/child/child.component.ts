import { Component, Input, OnInit, DoCheck, OnDestroy } from '@angular/core';


@Component({
  selector: 'app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements OnInit, DoCheck, OnDestroy {

  @Input()
  childInputField: string;
  childField: string="child value";

  ngOnInit(){
    console.log('Child component initialised. ');
  }

  ngDoCheck(){
    console.log('Child component ngDoCheck called. ');


  }

  ngOnDestroy(){
    console.log('Child component destroyed. ');
  }  

 }
