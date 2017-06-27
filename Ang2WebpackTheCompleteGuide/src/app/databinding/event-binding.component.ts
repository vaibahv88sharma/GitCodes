import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'fa-event-binding',
  template: `
    <button (click)="onClicked()">Click Button!</button>
    <input type='button' (click)="onClicked()" value="Click Input Element!" />
  `,
  styles: []
})
export class EventBindingComponent {

  @Output() clicked = new EventEmitter<string>();

  onClicked(){
    this.clicked.emit('It works!');
  }

}
