import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class BaseDataService {

  constructor(
    private accountService: AccountService,
    private httpClient: HttpClient
  ) { }

  getGenders(): Observable<string[]> {
    return this.getCards('genders');
  }

  getDungeons(): Observable<string[]> {
    return this.getCards('dungeons');
  }

  getRaces(): Observable<string[]> {
    return this.getCards('races');
  }

  getClasses(): Observable<string[]> {
    return this.getCards('classes');
  }

  getMonsters(): Observable<string[]> {
    return this.getCards('monsters');
  }

  getCurses(): Observable<string[]> {
    return this.getCards('curses');
  }

  getCards(cardType: string): Observable<string[]> {
    return this.accountService.get<string[]>(environment.gameApiUrl + 'cards/' + cardType);
  }
}
