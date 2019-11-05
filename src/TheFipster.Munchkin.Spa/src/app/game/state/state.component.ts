import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StateService } from 'src/app/services/state.service';
import { Score } from 'src/app/interfaces/score';
import { GameState } from 'src/app/enums/game-state.enum';

@Component({
  selector: 'app-state',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.scss']
})
export class StateComponent implements OnInit {
  private gameId: string;

  score: Score;
  gameState = GameState.Setup;
  gameStates = GameState;

  constructor(
    private route: ActivatedRoute,
    private state: StateService
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.gameId = params.get('gameId');
      this.state.sync(this.gameId);
    });

    this.state.score.subscribe((score: Score) => {
      this.score = score;
      this.setState();
      console.log(this.score);
      console.log(this.gameState);
    });
  }

  setState() {
    this.gameState = this.state.gameState;
  }

}
