import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-dungeon',
  templateUrl: './dungeon.component.html',
  styleUrls: ['./dungeon.component.scss']
})
export class DungeonComponent implements OnInit {
  @Input() dungeons: string[];
  constructor() { }

  ngOnInit() {
  }

}
