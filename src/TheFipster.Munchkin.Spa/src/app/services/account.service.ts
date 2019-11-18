import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserManager, User } from 'oidc-client';
import { from } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userManager: UserManager;
  private isUserAuthenticatedSubject = new BehaviorSubject<User>(null);
  isUserAuthenticated: Observable<User> = this.isUserAuthenticatedSubject.asObservable();
  private user: User = null;

  constructor(
    private http: HttpClient
  ) {
    this.userManager = new UserManager(environment.identity);
  }

  updateUserAuthenticationStatus() {
    const promise = this.userManager.getUser().then((user: User) => {
      if (user) {
        this.isUserAuthenticatedSubject.next(user);
        this.user = user;
        console.log('User logged in', user.profile);
      } else {
        this.isUserAuthenticatedSubject.next(null);
        console.log('User not logged in');
      }
    });

    return from(promise);
  }

  setUserAsNotAuthenticated() {
    this.isUserAuthenticatedSubject.next(null);
  }

  login() {
    this.userManager.signinRedirect();
  }

  callback() {
    console.log(window.location);
    (new UserManager({ response_mode: 'query' })).signinRedirectCallback().then((user: User) => {
      this.isUserAuthenticatedSubject.next(user);
      this.user = user;
    }).catch((e) => {
      console.log(e);
      this.isUserAuthenticatedSubject.next(null);
    });
  }

  logout() {
    this.userManager.signoutRedirect();
  }

  get<T>(url: string): Observable<T> {
    console.log('getting url: ' + url);
    if (this.user == null) {
      console.log('Not authenticated for querying');
      return null;
    }

    return this.http.get<T>(url, this.httpOptions());
  }

  post<T>(url: string, body: T) {
    console.log('posting url: ' + url);
    console.log(body);
    if (this.user == null) {
      console.log('Not authenticated for querying');
      return null;
    }

    return this.http.post(url, body, this.httpOptions());
  }

  put<T>(url: string, body: T) {
    console.log('putting url: ' + url);
    console.log(body);
    if (this.user == null) {
      console.log('Not authenticated for querying');
      return null;
    }

    return this.http.put(url, body, this.httpOptions());
  }

  private httpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + this.user.access_token
      })
    };
  }
}
