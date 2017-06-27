import { Directive, HostListener, HostBinding, Input, OnInit } from '@angular/core';
//import {Directive, ElementRef, Renderer} from '@angular/core';

@Directive({
  selector: '[highlight]'
})
export class HighlightDirective {

    @HostListener('mouseenter') mouseover() {
        this.backgroundColor = this.highlightColor;
    };
    @HostListener('mouseleave') mouseleave() {
        this.backgroundColor = this.defaultColor;
    };
    @HostBinding('style.backgroundColor') get setColor() {
        return this.backgroundColor;
    };
    @Input() defaultColor = 'white';
    @Input('highlight') highlightColor = 'green';

    private backgroundColor: string;

    constructor() {
    }
    //constructor(private elementRef: ElementRef, private renderer: Renderer) {
    //    //this.elementRef.nativeElement.style.backgroundColor = 'green';
    //    this.renderer.setElementStyle(this.elementRef.nativeElement, 'background-color', 'green');
    //}
    ngOnInit() {
        this.backgroundColor = this.defaultColor;
    }
}

