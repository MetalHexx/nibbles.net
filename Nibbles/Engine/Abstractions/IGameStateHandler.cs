using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine.Abstractions
{
    public interface IGameStateHandler
    {
        event Action? GameOver;
        void CreateFood();
        void IncrementMoveScore();
        void UpdateState(PositionTransform transform, long renderDelta);
        void SnakeShoot();
    }
}