import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthService } from './core/auth.service';
import { OidcSecurityService, AuthModule } from 'angular-auth-oidc-client';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot()
  ],
  providers: [
    AuthService,
    OidcSecurityService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
