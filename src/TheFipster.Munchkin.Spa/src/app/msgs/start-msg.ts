import { GameMsg } from './game-msg';

export class StartMsg extends GameMsg {
    private constructor() {
        super();
        this.type = 'StartMessage';
     }

     static create(): StartMsg {
         return new StartMsg();
     }
 }
