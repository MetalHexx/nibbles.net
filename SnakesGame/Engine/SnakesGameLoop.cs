﻿using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakesGame.Engine
{
    public class SnakesGameLoop : GameLoop
    {
        public SnakesGameLoop(ISpriteRenderer renderer, IGameStateReducer game) : base(renderer, game) { }
    }
}
