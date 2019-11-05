import { Component, OnInit, Input } from '@angular/core';
import { Hero } from 'src/app/interfaces/hero';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { SelectChooserComponent } from 'src/app/sheets/select-chooser/select-chooser.component';
import { BaseDataService } from 'src/app/services/base-data.service';
import { MasterService } from 'src/app/services/master.service';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.scss']
})
export class HeroComponent implements OnInit {
  @Input() hero: Hero;

  races: string[];
  classes: string[];
  dungeons: string[];
  genders: string[];
  monsters: string[];
  curses: string[];

  constructor(
    private baseData: BaseDataService,
    private bottomSheet: MatBottomSheet,
    private masterService: MasterService
  ) { }

  ngOnInit() {
    this.baseData.getRaces().subscribe((races: string[]) => this.races = races);
    this.baseData.getClasses().subscribe((classes: string[]) => this.classes = classes);
    this.baseData.getDungeons().subscribe((dungeons: string[]) => this.dungeons = dungeons);
    this.baseData.getGenders().subscribe((genders: string[]) => this.genders = genders);
    this.baseData.getMonsters().subscribe((monsters: string[]) => this.monsters = monsters);
    this.baseData.getCurses().subscribe((curses: string[]) => this.curses = curses);
  }

  changeStrength() {
  }

  changeGender() {
    this.bottomSheet.open(SelectChooserComponent, {
      data: { title: 'Geschlechtsumwandlung', options: this.genders, values: [ this.hero.player.gender ] }
    })
    .afterDismissed()
    .subscribe((result: any) => {
      console.log('Dismissed');
      console.log(result);
    });
  }

  changeRace() {
    this.bottomSheet.open(SelectChooserComponent, {
      data: { title: 'Rassenwechsel', options: this.races, values: this.hero.races }
    })
    .afterDismissed()
    .subscribe((result: any) => {
      this.masterService.changeRace(this.hero.player.id, result.add, result.remove);
    });
  }

  changeClass() {
    this.bottomSheet.open(SelectChooserComponent, {
      data: { title: 'Klassenwechsel', options: this.classes, values: this.hero.classes }
    })
    .afterDismissed()
    .subscribe((result: any) => {
      this.masterService.changeClass(this.hero.player.id, result.add, result.remove);
    });
  }
}
