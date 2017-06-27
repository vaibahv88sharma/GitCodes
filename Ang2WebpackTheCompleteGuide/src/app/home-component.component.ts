import { Component, OnDestroy } from '@angular/core';
import { Router } from "@angular/router";
import { Subscription } from "rxjs/Rx";

@Component({
  selector: 'app-home-component',
  templateUrl: './home-component.component.html',
  styleUrls: ['./home-component.component.css']
})
export class HomeComponent implements OnDestroy {

    private subscription: Subscription;

    param: string;

    constructor(private router: Router) {
        this.subscription = router.routerState.root.queryParams.subscribe(
            (queryParam: any) => this.param = queryParam['analytics']
        );
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

}
