﻿using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class GameMessage
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
