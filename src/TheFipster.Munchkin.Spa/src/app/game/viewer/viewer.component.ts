import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { InitGameService } from 'src/app/services/init-game.service';

@Component({
  selector: 'app-viewer',
  templateUrl: './viewer.component.html',
  styleUrls: ['./viewer.component.scss']
})
export class ViewerComponent implements OnInit {
  public initCode: string;

  constructor(
    private router: Router,
    private initGame: InitGameService
  ) { }

  ngOnInit() {
    this.initGame.initCode.subscribe((initCode: string) => {
      if (initCode) {
        this.initCode = initCode;
        this.waitForGameMaster(initCode);
      }
    });
  }

  init() {
    this.initGame.startInitialization();
  }

  private waitForGameMaster(initCode: string) {
    this.initGame.waitForVerification(initCode);
    this.initGame.isGameVerified.subscribe((gameId: string) => {
      if (gameId) {
        this.router.navigate(['state', gameId]);
      }
    });
  }
}
