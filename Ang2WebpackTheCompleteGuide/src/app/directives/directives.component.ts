import { Component, OnInit } from '@angular/core';
import { HighlightDirective } from './highlight.directive';
import { UnlessDirective } from './unless.directive';


@Component({
  selector: 'app-directives',
  templateUrl: './directives.component.html',
  styleUrls: ['./directives.component.css']
})
export class DirectivesComponent implements OnInit {

  constructor() { }

  private switch = true;
  private items = [1, 2, 3, 4, 5];
  private value = 100;
  onSwitch() {
      this.switch = !this.switch;
  }

  ngOnInit() {
  }

}
