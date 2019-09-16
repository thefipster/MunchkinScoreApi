using System;
using System.Collections.Generic;
using System.Text;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class LevelAction : LevelMessage, IGameAction
    {
        public void ApplyTo(GameState state)
        {

        }

        public void RevertFrom(GameState state)
        {

        }
    }
}
