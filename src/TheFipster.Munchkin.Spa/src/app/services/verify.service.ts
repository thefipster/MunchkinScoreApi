import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VerifyService {

  constructor(
    private httpClient: HttpClient
  ) { }

  checkInitCode(initCode: string) {
    return this.httpClient.get(environment.gameApiUrl + 'game/verify/' + initCode, { withCredentials: true });
  }
}
