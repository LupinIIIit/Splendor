import { Inject, Injectable, Optional } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams,HttpResponse, HttpEvent} from '@angular/common/http';
import { CustomHttpUrlEncodingCodec } from '../encoder';
import { Observable } from 'rxjs/Observable';
import { ApplicationConfiguration } from '../models/applicationConfiguration';
import { BASE_PATH, COLLECTION_FORMATS } from '../variables';
import { Configuration } from '../configuration';
@Injectable()
export class ConfigurationService {
    protected basePath = 'https://localhost';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();
    constructor(protected httpClient: HttpClient, @Optional() @Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
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


    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiApplicationstatsGet(observe?: 'body', reportProgress?: boolean): Observable<any>;
    public apiApplicationstatsGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public apiApplicationstatsGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public apiApplicationstatsGet(observe: any = 'body', reportProgress: boolean = false): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];

        return this.httpClient.get<any>(`${this.basePath}/api/applicationstats`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiConfigurationGet(observe?: 'body', reportProgress?: boolean): Observable<ApplicationConfiguration>;
    public apiConfigurationGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<ApplicationConfiguration>>;
    public apiConfigurationGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<ApplicationConfiguration>>;
    public apiConfigurationGet(observe: any = 'body', reportProgress: boolean = false): Observable<any> {

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

        return this.httpClient.get<ApplicationConfiguration>(`${this.basePath}/api/configuration`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public wellKnownAcmeChallengeByIdGet(id: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public wellKnownAcmeChallengeByIdGet(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public wellKnownAcmeChallengeByIdGet(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public wellKnownAcmeChallengeByIdGet(id: string, observe: any = 'body', reportProgress: boolean = false): Observable<any> {
        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling wellKnownAcmeChallengeByIdGet.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];

        return this.httpClient.get<any>(`${this.basePath}/.well-known/acme-challenge/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
