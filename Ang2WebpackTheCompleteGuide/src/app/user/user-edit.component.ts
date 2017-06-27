import { Component } from '@angular/core';
import { Router } from "@angular/router";
import { Observable } from "rxjs/Rx";
import { ComponentCanDeactivate } from "./user-edit.guard";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements ComponentCanDeactivate {
    done: false;
  constructor(private router: Router) { }

  onNavigate() {
      this.router.navigate(['/']);
  }

  canDeactivate(): Observable<boolean> | boolean {
      if (!this.done) {
          return confirm('Do you want to leave?');
      }
      return true;
  }

}
