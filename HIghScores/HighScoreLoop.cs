using Nibbles.Engine;
using Nibbles.Engine.Abstractions;

internal class HighScoreLoop : GameLoop
{
    public HighScoreLoop(ISpriteRenderer renderer, IGameStateReducer game) : base(renderer, game) { }
}