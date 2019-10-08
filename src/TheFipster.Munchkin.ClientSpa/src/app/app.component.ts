import { Component } from '@angular/core';
import { AuthService } from './core/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isAuthorized = false;

  constructor(
    private authService: AuthService
  ) {
    this.authService.getIsAuthorized().subscribe(
      (isAuthorized: boolean) => {
        this.isAuthorized = isAuthorized;
      });
  }

  login() {
    this.authService.login();
  }

  logout() {
    this.authService.logout();
  }
}
