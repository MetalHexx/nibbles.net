using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine.Abstractions
{
    public interface IGameStateHandler
    {
        event Action? GameOver;        
        void PlayerMove();
        void PlayerShoot();
        void UpdateState(PositionTransform transform, long renderDelta);
    }
}