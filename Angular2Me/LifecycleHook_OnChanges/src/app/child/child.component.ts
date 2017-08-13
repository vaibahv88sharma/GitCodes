import { Component, OnChanges, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';

@Component({
  selector: 'my-app-child',
  templateUrl: './child.component.html',
  styleUrls: ['./child.component.css']
})
export class ChildComponent implements OnChanges{ 

  @Input()
  valueToChange: any;

  @Output()
  resetValueAtPresent = new EventEmitter();

  @Output()  
  resetReferenceOnParent = new EventEmitter();

  @Output()  
  parentValueChanged = new EventEmitter();

  ngOnChanges(changes: SimpleChanges) {
    console.log(JSON.stringify(changes));
  }

  increment(){
    this.valueToChange.numberToChange +=1;
  }

  resetToParentValue(){
    this.resetValueAtPresent.emit();
  }

  resetParentReference(){
    this.resetReferenceOnParent.emit();
  }

  saveValueToParent(){
    this.parentValueChanged.emit(this.valueToChange.numberToChange);
  }

}
