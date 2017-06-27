import { Injectable, EventEmitter } from '@angular/core';
import { LogService } from './log.service';

@Injectable()
export class DataService {
    pushedData = new EventEmitter<string>();
    private data: string[] = [];

    addData(input: string) {
        this.data.push(input);
        this.logService.writeToLog('Saved item: ' + input);
    }

    getData() {
        return this.data;
    }

    pushData(value: string) {
        this.pushedData.emit(value);
    }

    constructor(private logService: LogService) { }

}
