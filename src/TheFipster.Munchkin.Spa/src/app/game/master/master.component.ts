import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MasterService } from 'src/app/services/master.service';
import { GameState } from 'src/app/enums/game-state.enum';
import { Game } from 'src/app/interfaces/game';

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.scss']
})
export class MasterComponent implements OnInit {
  private gameId: string;

  game: Game;
  gameState = GameState.Setup;
  gameStates = GameState;

  constructor(
    private route: ActivatedRoute,
    private masterService: MasterService
  ) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.gameId = params.get('gameId');
      this.masterService.sync(this.gameId);
    });

    this.masterService.game.subscribe((game: Game) => {
      this.game = game;
      this.setState();
    });
  }

  setState() {
    this.gameState = this.masterService.state;
  }
}
