import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { User } from 'oidc-client';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent implements OnInit {
  public isAuthenticated: boolean;

  constructor(
    public accountService: AccountService
  ) { }

  ngOnInit() {
    this.accountService.isUserAuthenticated.subscribe((user: User) => {
      if (user) {
        this.isAuthenticated = true;
        console.log('Authenticated');
        console.log(user);
      } else {
        this.isAuthenticated = false;
      }
    });
  }
}
