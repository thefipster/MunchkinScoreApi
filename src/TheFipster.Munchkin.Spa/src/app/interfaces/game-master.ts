import { Player } from './player';

export interface GameMaster extends Player {
    email: string;
    playerPool: Player[];
}
