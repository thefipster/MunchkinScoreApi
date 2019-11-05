import { ModifierMsg } from './modifier-msg';
import { MsgModifier } from '../enums/msg-modifier.enum';

export class DungeonMsg extends ModifierMsg<string> {
    public dungeon: string;

    private constructor(add: string[], remove: string[]) {
        super();
        this.add = add;
        this.remove = remove;
        this.type = 'DungeonMessage';
    }

    static createAdd(dungeonCard: string): DungeonMsg {
        return new DungeonMsg([ dungeonCard ], []);
    }

    static createRemove(dungeonCard: string): DungeonMsg {
        return new DungeonMsg([], [ dungeonCard ]);
    }
}
