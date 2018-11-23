import { Inject, Injectable, Optional } from '@angular/core';
import {
    HttpClient, HttpHeaders, HttpParams,
    HttpResponse, HttpEvent
} from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from '../encoder';
import { Observable } from 'rxjs/Observable';
import { Appuntamento } from '../models/appuntamento';
import { BASE_PATH, COLLECTION_FORMATS } from '../variables';
import { Configuration } from '../configuration';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageService } from './message.service';
import { format } from 'date-fns';
import { CalendarEvent } from 'angular-calendar';

@Injectable()
export class AppuntamentiService {

    protected basePath = 'https://localhost';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, protected messageService: MessageService, @Optional() @Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (let consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }
    private getDate(isFrom: boolean): string {
        let date = new Date();
        let strDate = format(date,'YYYY-MM-DD');
        let hour = "";
        if (isFrom === true) {
            hour = " 00:00:00.000";
        } else {
            hour = " 23:59:59.999";
        }
        return strDate + hour;
    }
    /**
     * 
     * 
     * @param aziendaId 
     * @param dataDa 
     * @param dataA 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getList(aziendaId: number, dataDa?: string, dataA?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<Appuntamento>>;
    public getList(aziendaId: number, dataDa?: string, dataA?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Appuntamento>>>;
    public getList(aziendaId: number, dataDa?: string, dataA?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Appuntamento>>>;
    public getList(aziendaId: number, dataDa?: string, dataA?: string, observe: any = 'body', reportProgress: boolean = false): Observable<any> {
        if (aziendaId === null || aziendaId === undefined) {
            throw new Error('Required parameter aziendaId was null or undefined when calling getList.');
        }
        if (dataDa === null || dataDa === undefined) {
            dataDa = this.getDate(true);
        }
        if (dataA === null || dataA === undefined) {
            dataA = this.getDate(false);
        }
        let headers = this.defaultHeaders;
        let httpHeaderAccepts: string[] = ['text/plain', 'application/json', 'text/json'];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [];
        return this.httpClient.get<Array<Appuntamento>>(`${this.basePath}/api/v1/appuntamenti/${encodeURIComponent(String(aziendaId))}/${encodeURIComponent(String(dataDa))}/${encodeURIComponent(String(dataA))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(
            tap(appuntamenti => this.log(`fetched appuntamenti`)),
            catchError(this.handleError('getListByAziendaIdDate', []))
        );
    }
    /**
     * 
     * 
     * @param aziendaId 
     * @param page 
     * @param pageSize 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getListPaginated(aziendaId: number, page: number, pageSize: number, observe?: 'body', reportProgress?: boolean): Observable<Array<Appuntamento>>;
    public getListPaginated(aziendaId: number, page: number, pageSize: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<Appuntamento>>>;
    public getListPaginated(aziendaId: number, page: number, pageSize: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<Appuntamento>>>;
    public getListPaginated(aziendaId: number, page: number, pageSize: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {
        if (aziendaId === null || aziendaId === undefined) {
            throw new Error('Required parameter aziendaId was null or undefined when calling apiV1AppuntamentiByAziendaIdGet.');
        }
        if (page === null || page === undefined) {
            throw new Error('Required parameter page was null or undefined when calling apiV1AppuntamentiByAziendaIdGet.');
        }
        if (pageSize === null || pageSize === undefined) {
            throw new Error('Required parameter pageSize was null or undefined when calling apiV1AppuntamentiByAziendaIdGet.');
        }

        let queryParameters = new HttpParams({ encoder: new CustomHttpUrlEncodingCodec() });
        if (page !== undefined) {
            queryParameters = queryParameters.set('page', <any>page);
        }
        if (pageSize !== undefined) {
            queryParameters = queryParameters.set('pageSize', <any>pageSize);
        }
        console.log("dentro il methodo di chiamata");
        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];
        return this.httpClient.get<Array<Appuntamento>>(`${this.basePath}/api/v1/appuntamenti/${encodeURIComponent(String(aziendaId))}`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(
            tap(appuntamenti => this.log(`fetched appuntamenti`)),
            catchError(this.handleError('getListByAziendaIdDate', []))
        );
    }
    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public delete(id: number, observe?: 'body', reportProgress?: boolean): Observable<boolean>;
    public delete(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<boolean>>;
    public delete(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<boolean>>;
    public delete(id: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {
        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiV1AppuntamentiByIdDelete.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];

        return this.httpClient.delete<boolean>(`${this.basePath}/api/v1/appuntamenti/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(
            tap(appuntamenti => this.log(`fetched appuntamenti`)),
            catchError(this.handleError('getListByAziendaIdDate', []))
        );
    }

    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiV1AppuntamentoByIdGet(id: number, observe?: 'body', reportProgress?: boolean): Observable<Appuntamento>;
    public apiV1AppuntamentoByIdGet(id: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Appuntamento>>;
    public apiV1AppuntamentoByIdGet(id: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Appuntamento>>;
    public apiV1AppuntamentoByIdGet(id: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {
        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling apiV1AppuntamentoByIdGet.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];

        return this.httpClient.get<Appuntamento>(`${this.basePath}/api/v1/appuntamento/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(
            tap(appuntamenti => this.log(`fetched appuntamenti`)),
            catchError(this.handleError('getListByAziendaIdDate', []))
        );
    }

    /**
     * 
     * 
     * @param postedApp 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public post(postedApp?: Appuntamento, observe?: 'body', reportProgress?: boolean): Observable<Appuntamento>;
    public post(postedApp?: Appuntamento, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Appuntamento>>;
    public post(postedApp?: Appuntamento, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Appuntamento>>;
    public post(postedApp?: Appuntamento, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
            'application/json-patch+json',
            'application/json',
            'text/json',
            'application/_*+json'
        ];
        let httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set("Content-Type", httpContentTypeSelected);
        }

        return this.httpClient.post<Appuntamento>(`${this.basePath}/api/v1/appuntamento`,
            postedApp,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        ).pipe(
            tap(appuntamenti => this.log(`fetched appuntamenti`)),
            catchError(this.handleError('getListByAziendaIdDate', []))
        );
    }
    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {

            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead

            // TODO: better job of transforming error for user consumption
            this.log(`${operation} failed: ${error.message}`);

            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }

    /** Log a HeroService message with the MessageService */
    private log(message: string) {
        this.messageService.add('HeroService: ' + message);
    }
}
