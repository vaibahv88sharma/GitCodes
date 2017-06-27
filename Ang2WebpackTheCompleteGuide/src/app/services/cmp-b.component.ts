import { Component, OnInit } from '@angular/core';
import { LogService } from './log.service';
import { DataService } from './data.service';

@Component({
  selector: 'app-cmp-b',
  templateUrl: './cmp-b.component.html',
  styleUrls: ['./cmp-b.component.css']//,
  //providers: [LogService]
})
export class CmpBComponent implements OnInit {
//export class CmpBComponent {

    value = '';
    items: string[] = [];
    constructor(private logService: LogService, private dataService: DataService) { }

    onLog(value: string) {
        this.logService.writeToLog(value);
    }

    onStore(value: string) {
        this.dataService.addData(value);
    }

    onGet() {
        this.items = this.dataService.getData().slice(0);
    }

    ngOnInit() {
        this.dataService.pushedData.subscribe(
            (data: any) => this.value = data
        );
    }

}
