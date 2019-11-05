import { ModifierMsg } from './modifier-msg';

export class RaceMsg extends ModifierMsg<string> {
    playerId: string;

    constructor(playerId: string, add: string[], remove: string[]) {
        super();
        this.playerId = playerId;
        this.add = add;
        this.remove = remove;
        this.type = 'RaceMessage';
    }

    static create(playerId: string, add: string[], remove: string[]): RaceMsg {
        return new RaceMsg(playerId, add, remove);
    }
}
