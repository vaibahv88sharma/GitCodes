import {Alumno}             from '../models/alumno'
import {Http, Headers}      from 'angular2/http'
import {Injectable}         from 'angular2/core'
import 'rxjs/add/operator/map'

@Injectable()
export class AlumnoService {

    private spApiUrl: string;
    private spListName: string;

    constructor(private http: Http) {
        this.spListName = "Alumno";
        this.spApiUrl = _spPageContextInfo.webServerRelativeUrl + "/_api/web/lists/getByTitle('" +;
    }
}
