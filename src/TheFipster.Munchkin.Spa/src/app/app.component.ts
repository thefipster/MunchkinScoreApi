import { Component } from '@angular/core';
import { AccountService } from './services/account.service';
import { User } from 'oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public isAuthenticated: boolean;

  constructor(
    private account: AccountService
  ) {
    account.isUserAuthenticated.subscribe((user: User) => {
      if (user) {
        this.isAuthenticated = true;
      } else {
        this.isAuthenticated = false;
      }
    });

    account.updateUserAuthenticationStatus();
  }

  login() {
    this.account.login();
  }

  logout() {
    this.account.logout();
  }
}
