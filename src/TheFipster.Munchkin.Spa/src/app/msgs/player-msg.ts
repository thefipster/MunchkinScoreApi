import { ModifierMsg } from './modifier-msg';
import { Player } from '../interfaces/player';

export class PlayerMsg extends ModifierMsg<Player> {
    public player: Player;

    private constructor(add: Player[], remove: Player[]) {
        super();
        this.add = add;
        this.remove = remove;
        this.type = 'PlayerMessage';
    }

    static createAdd(player: Player): PlayerMsg {
        return new PlayerMsg([ player ], []);
    }

    static createRemove(player: Player): PlayerMsg {
        return new PlayerMsg([], [ player ]);
    }
}
