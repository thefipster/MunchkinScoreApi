import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GameMaster } from '../interfaces/game-master';
import { Player } from '../interfaces/player';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  constructor(
    private accountService: AccountService
  ) { }

  getProfile() {
    return this.accountService.get<GameMaster>(environment.gameApiUrl + 'player');
  }

  getPlayerPool() {
    return this.accountService.get<Player[]>(environment.gameApiUrl + 'player/pool');
  }

  postProfile(profile: GameMaster) {
    return this.accountService
      .post(environment.gameApiUrl + 'player/update', profile);
  }

  postFriend(name: string, gender: string) {
    const player = { name, gender } as Player;
    return this.accountService
      .post(environment.gameApiUrl + 'player/friend', player);
  }
}
