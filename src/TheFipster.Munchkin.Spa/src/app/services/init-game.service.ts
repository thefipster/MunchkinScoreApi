import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitGameService {
  private isGameVerifiedSubject = new BehaviorSubject<string>(null);
  isGameVerified: Observable<string> = this.isGameVerifiedSubject.asObservable();

  private initCodeSubject = new BehaviorSubject<string>(null);
  initCode: Observable<string> = this.initCodeSubject.asObservable();

  constructor(
    private httpClient: HttpClient
  ) { }

  startInitialization() {
    this.httpClient.get<string>(environment.gameApiUrl + 'game/init').subscribe((result: any) => {
      this.initCodeSubject.next(result.initCode);
    });
  }

  waitForVerification(initCode: string) {
    this.httpClient.get<string>(this.getWaitUrl(initCode)).subscribe((result: any) => {
      this.isGameVerifiedSubject.next(result.gameId);
    });
  }

  getWaitUrl(initCode: string): string {
    return environment.gameApiUrl + 'game/wait/' + initCode;
  }
}
