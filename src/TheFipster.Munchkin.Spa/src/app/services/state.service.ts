import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { DungeonMsg } from '../msgs/dungeon-msg';
import { GameMsg } from '../msgs/game-msg';
import { BehaviorSubject, Observable } from 'rxjs';
import { Score } from '../interfaces/score';
import { GameState } from '../enums/game-state.enum';
import { interval } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  private gameId: string;
  private scoreSubject = new BehaviorSubject<Score>(null);
  score: Observable<Score> = this.scoreSubject.asObservable();
  protocol: GameMsg[] = [];
  currentScore: Score;
  gameState = GameState.Setup;


  constructor(
    private httpClient: HttpClient
  ) { }

  sync(gameId: string) {
    console.log('sync');
    this.gameId = gameId;
    this.score.subscribe((score: Score) => {
      this.setState(score);
      this.currentScore = score;
  });

    this.getState();
    this.pollState();
  }

  addToProtocol(msg: DungeonMsg) {
    this.protocol.push(msg);
  }

  private getState() {
    console.log('getting state');


    this.httpClient
      .get(environment.gameApiUrl + 'game/state/' + this.gameId)
      .subscribe((score: Score) => this.scoreSubject.next(score));
  }

  private pollState() {
    console.log('polling state');
    this.httpClient
      .get<Score>(environment.gameApiUrl + 'game/poll/' + this.gameId)
      .subscribe((score: Score) => {
        this.scoreSubject.next(score);
        this.pollState();
      });
  }

  private setState(score: Score): void {
    if (!score) {
      this.gameState = GameState.Setup;
      return;
    }

    if (score.begin) {
      this.gameState = GameState.Running;
    }

    if (score.end) {
      this.gameState = GameState.End;
    }
  }
}
