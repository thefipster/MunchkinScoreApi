import { TestBed } from '@angular/core/testing';

import { InitGameService } from './init-game.service';

describe('InitGameService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: InitGameService = TestBed.get(InitGameService);
    expect(service).toBeTruthy();
  });
});
