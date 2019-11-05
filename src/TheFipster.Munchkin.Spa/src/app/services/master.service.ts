import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { DungeonMsg } from '../msgs/dungeon-msg';
import { GameMsg } from '../msgs/game-msg';
import { GameState } from '../enums/game-state.enum';
import { PlayerMsg } from '../msgs/player-msg';
import { Player } from '../interfaces/player';
import { Game } from '../interfaces/game';
import { ClassMsg } from '../msgs/class-msg';
import { RaceMsg } from '../msgs/race-msg';

@Injectable({
  providedIn: 'root'
})
export class MasterService {
  private httpGameIdHeaderOptions = {
    headers: new HttpHeaders({
      'Munchkin-GameId': '',
    }),
    withCredentials: true
  };

  private gameId: string;
  private sequence = 0;
  private gameSubject = new BehaviorSubject<Game>(null);

  state: GameState = GameState.Setup;
  game: Observable<Game> = this.gameSubject.asObservable();
  gameState: Game;

  constructor(
    private httpClient: HttpClient
  ) {
    this.game.subscribe((game: Game) => {
      this.setState(game);
      this.gameState = game;
      if (game && this.sequence !== game.protocol.length) {
        console.log('Game got out of sync... but we managed it.');
        this.sequence = game.protocol.length;
      }
    });
  }

  sync(gameId: string) {
    this.gameId = gameId;
    this.setGameIdHeader(gameId);
    this.getScore(gameId);
  }

  addDungeon(dungeon: string) {
    const msg = DungeonMsg.createAdd(dungeon);
    this.sendMessages([ msg ]);
  }

  addPlayer(player: Player) {
    const msg = PlayerMsg.createAdd(player);
    this.sendMessages([ msg ]);
  }

  changeClass(playerId: string, add: string[], remove: string[]) {
    const msg = ClassMsg.create(playerId, add, remove);
    this.sendMessages([ msg ]);
  }

  changeRace(playerId: string, add: string[], remove: string[]) {
    const msg = RaceMsg.create(playerId, add, remove);
    this.sendMessages([ msg ]);
  }

  sendMessages(messages: GameMsg[]) {
    messages.forEach(msg => {
      msg.sequence = ++this.sequence;
    });

    this.httpClient
      .post(environment.gameApiUrl + 'game/append', messages, this.httpGameIdHeaderOptions)
      .subscribe((game: Game) => this.gameSubject.next(game));
  }

  private setState(game: Game): void {
    if (!game || !game.score) {
      this.state = GameState.Setup;
      return;
    }

    if (game.score.begin) {
      this.state = GameState.Running;
    }

    if (game.score.end) {
      this.state = GameState.End;
    }
  }

  private setGameIdHeader(gameId: string) {
    this.httpGameIdHeaderOptions.headers =
      this.httpGameIdHeaderOptions.headers.set('Munchkin-GameId', gameId);
  }

  private getScore(gameId: string) {
    this.httpClient
      .get<Game>(environment.gameApiUrl + 'game/state/' + gameId)
      .subscribe((game: Game) => this.gameSubject.next(game));
  }
}
