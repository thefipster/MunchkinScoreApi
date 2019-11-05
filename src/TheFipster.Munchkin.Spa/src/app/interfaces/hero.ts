import { Player } from './player';

export class Hero {
    player: Player;
    level: number;
    bonus: number;
    races: string[];
    classes: string[];

    constructor(player: Player) {
        this.player = player;
        this.level = 1;
        this.bonus = 0;
        this.races = [];
        this.classes = [];
    }
}
