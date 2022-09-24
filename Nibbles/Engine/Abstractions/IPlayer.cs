namespace Nibbles.Engine.Abstractions
{
    public interface IPlayer
    {
        ActionState ActionState { get; }
        MovingState MovingState { get; }
        MovingState LastMovingState { get; }
        PlayerState NextState();
    }
}