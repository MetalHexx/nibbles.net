namespace Nibbles.Engine
{
    public interface IPlayer
    {
        ActionState ActionState { get; }
        MovingState MovingState { get; }
        MovingState LastMovingState { get; }
        PlayerState NextState();
    }
}