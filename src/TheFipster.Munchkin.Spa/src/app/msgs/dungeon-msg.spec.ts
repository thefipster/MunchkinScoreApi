import { DungeonMsg } from './dungeon-msg';

describe('Dungeon', () => {
  it('should create an instance', () => {
    expect(new DungeonMsg()).toBeTruthy();
  });

  it('should create an additive instance', () => {
    const msg = DungeonMsg.createAdd('myId', 'myDungeon');
    expect(msg.modifier).toEqual('Add');
  });
});
