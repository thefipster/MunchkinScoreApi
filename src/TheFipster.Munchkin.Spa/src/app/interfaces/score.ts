import { Hero } from './hero';

export interface Score {
    begin: string;
    end: string;
    dungeons: string[];
    heroes: Hero[];
}
