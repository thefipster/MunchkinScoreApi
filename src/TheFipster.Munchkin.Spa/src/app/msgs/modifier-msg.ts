import { GameMsg } from './game-msg';

export class ModifierMsg<T> extends GameMsg {
    add: T[];
    remove: T[];
}
