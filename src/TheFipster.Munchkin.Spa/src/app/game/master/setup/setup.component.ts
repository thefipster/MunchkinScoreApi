import { Component, OnInit } from '@angular/core';
import { MasterService } from 'src/app/services/master.service';
import { BaseDataService } from 'src/app/services/base-data.service';
import { Player } from 'src/app/interfaces/player';
import { ProfileService } from 'src/app/services/profile.service';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { GameMaster } from 'src/app/interfaces/game-master';
import { GameMsg } from 'src/app/msgs/game-msg';
import { PlayerMsg } from 'src/app/msgs/player-msg';
import { DungeonMsg } from 'src/app/msgs/dungeon-msg';
import { StartMsg } from 'src/app/msgs/start-msg';

@Component({
  selector: 'app-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.scss']
})
export class SetupComponent implements OnInit {
  public gameMaster: GameMaster;
  public dungeons: string[] = [];
  public selectedDungeon: string;
  public selectedPlayers: Player[] = [];
  public dungeonSelectionFinished: boolean;

  constructor(
    private masterService: MasterService,
    private baseData: BaseDataService,
    private profileService: ProfileService
  ) { }

  ngOnInit() {
    this.profileService.getProfile().subscribe(
      (gameMaster: GameMaster) => {
        this.gameMaster = gameMaster;
        this.selectedPlayers.push(gameMaster);
      });

    this.baseData.getDungeons().subscribe(
      (dungeons: string[]) => this.dungeons = dungeons);
  }

  togglePlayer(event: MatSlideToggleChange, playerId: string) {
    const player = this.gameMaster.playerPool.find(p => p.id === playerId);

    if (event.checked) {
      this.selectedPlayers.push(player);
    } else {
      this.selectedPlayers.forEach((item, index) => {
        if (item.id === playerId) {
          this.selectedPlayers.splice(index, 1);
        }
      });
    }
  }

  startAdventure() {
    const messageBundle: GameMsg[] = [];
    this.selectedPlayers.forEach((player: Player) => {
      const playerMsg = PlayerMsg.createAdd(player);
      messageBundle.push(playerMsg);
    });

    const dungeonMsg = DungeonMsg.createAdd(this.selectedDungeon);
    messageBundle.push(dungeonMsg);

    const startMsg = StartMsg.create();
    messageBundle.push(startMsg);

    this.masterService.sendMessages(messageBundle);
  }
}
