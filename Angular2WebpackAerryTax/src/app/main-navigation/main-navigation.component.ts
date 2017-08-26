import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-navigation',
  templateUrl: './main-navigation.component.html',
  styleUrls: ['./main-navigation.component.css']
})
export class MainNavigationComponent implements OnInit {

    isIn = false;   // store state

  show:boolean = false;

  toggleCollapse() {
    this.show = !this.show;
  }

    toggleState() { // click handler
        let bool = this.isIn;
        this.isIn = bool === false ? true : false; 
    }

  constructor(private _router: Router) { }

  isActive(url: string): boolean {
    //debugger;
    return url == this._router.url;
  }

  isOpen: boolean = false;
  
  dropdownMenu: Array<any> = [
    {
      text: 'Dynamic 1',
    },
    {
      text: 'Dynamic 2'
    }
  ];
  
  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }


/*             $scope.isPage = function (name) {
                return new RegExp("/" + name + "($|/)").test($location.path());
            }; */


  ngOnInit() {
  }

}
