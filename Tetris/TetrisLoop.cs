using Nibbles.Engine;
using Nibbles.Engine.Abstractions;

public class TetrisLoop : GameLoop
{
    public TetrisLoop(ISpriteRenderer renderer, IGameStateReducer game) : base(renderer, game) { }
}

