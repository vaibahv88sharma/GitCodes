import {AppComponent}       from './components/app.component'
import {bootstrap}          from 'angular/platform/browser'
import {HTTP_PROVIDERS}     from 'angular2/http'

bootstrap(AppComponent, [HTTP_PROVIDERS]);
