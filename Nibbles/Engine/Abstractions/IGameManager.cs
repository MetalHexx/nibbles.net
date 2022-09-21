using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine.Abstractions
{
    public interface IGameManager
    {
        Action? GameOver { get; set; }
        void PlayerMove();
        void PlayerShoot();
        void UpdateState(PositionTransform transform, long renderDelta);
    }
}