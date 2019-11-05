import { ModifierMsg } from './modifier-msg';

export class ClassMsg extends ModifierMsg<string> {
    playerId: string;

    constructor(playerId: string, add: string[], remove: string[]) {
        super();
        this.playerId = playerId;
        this.add = add;
        this.remove = remove;
        this.type = 'ClassMessage';
    }

    static create(playerId: string, add: string[], remove: string[]): ClassMsg {
        return new ClassMsg(playerId, add, remove);
    }
}
