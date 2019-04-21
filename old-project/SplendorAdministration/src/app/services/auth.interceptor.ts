import { HttpInterceptor, HttpRequest, HttpHandler, HttpUserEvent, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";
import { tap, finalize } from "rxjs/operators"
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({ providedIn: 'root' })
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const started = Date.now();
        let ok: string;
        if (req.headers.get('No-Auth') == "True") {
            return next.handle(req.clone());
        }
        if (localStorage.getItem('user_token') != null) {
            const clonedreq = req.clone({
                headers: req.headers.set("Authorization", "Bearer " + localStorage.getItem('userToken'))
            });
            return next.handle(clonedreq)
                .pipe(
                    tap(
                        succ => { },
                        err => {
                            if (err.status === 401)
                                this.router.navigateByUrl('/login');
                        }),
                    // Log when response observable either completes or errors
                    finalize(() => {
                        const elapsed = Date.now() - started;
                        const msg = `${req.method} "${req.urlWithParams}"
                     ${ok} in ${elapsed} ms.`;
                        console.log(msg);
                        //this.messenger.add(msg);
                    })
                );
        } else {
            this.router.navigateByUrl('/login');
        }
    }
}
