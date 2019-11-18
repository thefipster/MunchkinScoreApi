import { GameMsg } from '../msgs/game-msg';
import { Score } from './score';

export class Game {
    created: Date;
    id: string;
    protocol: GameMsg[];
    score: Score;
}
