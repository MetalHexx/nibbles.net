using Nibbles.GameObject.Dimensions;

namespace Nibbles.Player
{
    public interface IPlayerInput
    {
        ActionState ActionState { get; }
        MovingState MoveState { get; }

        event Action? Moved;
        event Action? Shot;

        PositionTransform GetMove();
        void UpdateState();
    }
}