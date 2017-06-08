import 
{  Component, 
          OnChanges,
          OnInit,
          DoCheck,
          AfterContentInit,
          AfterContentChecked,
          AfterViewInit,
          AfterViewChecked,
          OnDestroy,
          Input,
          ViewChild,
          ContentChild
} from '@angular/core';

@Component({
  selector: 'fa-lifecycle',
  template: `
    <ng-content></ng-content>
    <hr>
    <p #boundParagraph>{{bindable}}</p>
  `,
  styles: []
})
export class LifecycleComponent implements OnChanges,OnInit,DoCheck,AfterContentInit,AfterContentChecked,AfterViewInit,AfterViewChecked,OnDestroy  {

  @Input() 
  bindable= 1000;

  @ViewChild('boundParagraph')
  boundParagraph: HTMLElement;

  @ContentChild('boundContent')
  boundContent: HTMLElement;

  constructor() { }

  ngOnChanges() {
    this.log('1 ngOnChanges');
  }
  ngOnInit() {
    this.log('2 ngOnInit');
  }
  ngDoCheck() {
    this.log('3 ngDoCheck');
  }
  ngAfterContentInit() {
    this.log('4 ngAfterContentInit');
    console.log(this.boundContent);
  }
  ngAfterContentChecked() {
    this.log('5 ngAfterContentChecked');
  }
  ngAfterViewInit() {
    this.log('6 ngAfterViewInit');
    console.log(this.boundParagraph);
  }
  ngAfterViewChecked() {
    this.log('7 ngAfterViewChecked');
  }
  ngOnDestroy() {
    this.log('8 ngOnDestroy');
  }

  private log(hook: string){
    console.log(hook);
  }
}
