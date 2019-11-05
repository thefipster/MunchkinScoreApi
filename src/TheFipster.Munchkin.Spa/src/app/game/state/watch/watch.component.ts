import { Component, OnInit } from '@angular/core';
import { StateService } from 'src/app/services/state.service';
import { Score } from 'src/app/interfaces/score';

@Component({
  selector: 'app-watch',
  templateUrl: './watch.component.html',
  styleUrls: ['./watch.component.scss']
})
export class WatchComponent implements OnInit {
  score: Score;

  constructor(
    private stateService: StateService,
  ) { }

  ngOnInit() {
    this.score = this.stateService.currentScore;
    this.stateService.score.subscribe((score: Score) => this.score = score);
  }
}
