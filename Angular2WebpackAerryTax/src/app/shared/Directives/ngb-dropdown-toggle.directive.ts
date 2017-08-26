import { Directive, HostListener } from '@angular/core';
import { NgbDropdownDirective } from './ngb-dropdown.directive'

@Directive({
  selector: '[ngb-dropdown-toggle]',
  /* host: {'class': 'dropdown-toggle', 'aria-haspopup': 'true', '[attr.aria-expanded]': '_dropdown.open'} */
  host: {'class': 'dropdown-toggle', '[attr.aria-expanded]': '_dropdown.open'}
})
export class NgbDropdownToggleDirective {

  constructor(private _dropdown: NgbDropdownDirective) {}

  @HostListener('click')
  toggleOpen() {
    //debugger;
    this._dropdown.open = !this._dropdown.open;
  }
/*   @HostListener('mouseleave')
  toggleOpen1() {
    debugger;
    this._dropdown.open = !this._dropdown.open;
  }
   @HostListener('mouseenter')
  toggleOpen2() {
    this._dropdown.open = !this._dropdown.open;
  } */  
}
