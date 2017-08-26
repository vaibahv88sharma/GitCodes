import { Directive, Input, HostListener } from '@angular/core';

@Directive({
  selector: '[ngb-dropdown]',
  host: {'class': 'dropdown', '[class.open]': 'open'}
})
export class NgbDropdownDirective {

  @Input() open = true;

  constructor() { }

}
