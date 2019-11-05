import { Component, OnInit } from '@angular/core';
import { MasterService } from 'src/app/services/master.service';
import { Score } from 'src/app/interfaces/score';
import { Game } from 'src/app/interfaces/game';

@Component({
  selector: 'app-play',
  templateUrl: './play.component.html',
  styleUrls: ['./play.component.scss']
})
export class PlayComponent implements OnInit {
  game: Game;

  constructor(
    private masterService: MasterService,
  ) { }

  ngOnInit() {
    this.game = this.masterService.gameState;
    this.masterService.game.subscribe((game: Game) => this.game = game);
  }
}
