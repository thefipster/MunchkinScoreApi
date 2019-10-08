import { Injectable, OnDestroy, Inject } from '@angular/core';
import {
  OidcSecurityService,
  OpenIdConfiguration,
  AuthWellKnownEndpoints,
  AuthorizationResult,
  AuthorizationState
} from 'angular-auth-oidc-client';
import { Observable, Subscription, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable()
export class AuthService implements OnDestroy {
  isAuthorized = false;

  constructor(
    private oidcSecurityService: OidcSecurityService,
    private http: HttpClient,
    private router: Router,
  ) {
  }

  private isAuthorizedSubscription: Subscription = new Subscription();

  ngOnDestroy(): void {
    if (this.isAuthorizedSubscription) {
      this.isAuthorizedSubscription.unsubscribe();
    }
  }

  public initAuth() {
    const openIdConfiguration: OpenIdConfiguration = {
      stsServer: 'https://localhost:5001',
      client_id: 'client-spa',
      post_login_route: 'http://localhost:4200',
      response_type: 'code',
      scope: 'openid profile email sample-api',
      post_logout_redirect_uri: 'http://localhost:4200',
    };

    const authWellKnownEndpoints: AuthWellKnownEndpoints = {
      issuer: 'https://localhost:5001',
      jwks_uri: 'https://localhost:5001/.well-known/openid-configuration/jwks',
      authorization_endpoint: 'https://localhost:5001/connect/authorize',
      token_endpoint: 'https://localhost:5001/connect/token',
      userinfo_endpoint: 'https://localhost:5001/connect/userinfo',
      end_session_endpoint: 'https://localhost:5001/connect/endsession',
      check_session_iframe: 'https://localhost:5001/connect/checksession',
      revocation_endpoint: 'https://localhost:5001/connect/revocation',
      introspection_endpoint: 'https://localhost:5001/connect/introspect',
    };

    this.oidcSecurityService.setupModule(openIdConfiguration, authWellKnownEndpoints);

    if (this.oidcSecurityService.moduleSetup) {
      this.doCallbackLogicIfRequired();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.doCallbackLogicIfRequired();
      });
    }
    this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe((isAuthorized => {
      this.isAuthorized = isAuthorized;
    }));

    this.oidcSecurityService.onAuthorizationResult.subscribe(
      (authorizationResult: AuthorizationResult) => {
        this.onAuthorizationResultComplete(authorizationResult);
      });
  }

  private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {
    console.log('Auth result received AuthorizationState:'
      + authorizationResult.authorizationState
      + ' validationResult:' + authorizationResult.validationResult);

    if (authorizationResult.authorizationState === AuthorizationState.unauthorized) {
      if (window.parent) {
        // sent from the child iframe, for example the silent renew
        this.router.navigate(['/unauthorized']);
      } else {
        window.location.href = '/unauthorized';
      }
    }
  }

  private doCallbackLogicIfRequired() {
    this.oidcSecurityService.authorizedCallbackWithCode(window.location.toString());
  }

  getIsAuthorized(): Observable<boolean> {
    return this.oidcSecurityService.getIsAuthorized();
  }

  login() {
    console.log('start login');
    this.oidcSecurityService.authorize();
  }

  logout() {
    console.log('start logoff');
    this.oidcSecurityService.logoff();
  }

  get(url: string): Observable<any> {
    return this.http.get(url, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        this.oidcSecurityService.handleError(error);
        return throwError(error);
      }));
  }

  put(url: string, data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http.put(url, body, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        this.oidcSecurityService.handleError(error);
        return throwError(error);
      }));
  }

  delete(url: string): Observable<any> {
    return this.http.delete(url, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        this.oidcSecurityService.handleError(error);
        return throwError(error);
      }));
  }

  post(url: string, data: any): Observable<any> {
    const body = JSON.stringify(data);
    return this.http.post(url, body, { headers: this.getHeaders() })
      .pipe(catchError((error) => {
        this.oidcSecurityService.handleError(error);
        return throwError(error);
      }));
  }

  private getHeaders() {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json');
    return this.appendAuthHeader(headers);
  }

  public getToken() {
    const token = this.oidcSecurityService.getToken();
    return token;
  }

  private appendAuthHeader(headers: HttpHeaders) {
    const token = this.oidcSecurityService.getToken();

    if (token === '') { return headers; }

    const tokenValue = 'Bearer ' + token;
    return headers.set('Authorization', tokenValue);
  }
}