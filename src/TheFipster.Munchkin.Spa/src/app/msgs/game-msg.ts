import * as uuid from 'uuid';

export class GameMsg {
    id: string;
    timestamp: string;
    type: string;
    sequence: number;

    constructor() {
        this.id = uuid.v4();
        this.timestamp = new Date().toISOString();
    }
}
