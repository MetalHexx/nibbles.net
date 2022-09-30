using Nibbles.Engine;
using Nibbles.Engine.Abstractions;

namespace MainMenu
{
    public class GameMenuLoop : GameLoop
    {
        public GameMenuLoop(ISpriteRenderer renderer, IGameStateReducer game) : base(renderer, game) { }
    }
}
