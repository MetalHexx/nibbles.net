using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMenu
{
    public class GameMenuLoop : GameLoop
    {
        public GameMenuLoop(ISpriteRenderer renderer, IGameStateReducer game) : base(renderer, game) { }
    }
}
