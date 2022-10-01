using Nibbles.Engine;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine
{
    public enum MovingState
    {
        MovingLeft,
        MovingRight,
        MovingUp,
        MovingDown,
        Idle
    }

    public enum ActionState
    {
        Idle,
        Shooting,
        Quitting
    }

    public record PlayerState
    {
        public ActionState ActionState { get; init; }
        public MovingState MovingState { get; init; }
        public MovingState LastMovingState { get; init; }
        public MovingState LastNonIdleMovingState { get; init; }

        public PlayerState(ActionState actionState, MovingState lastMovingState, MovingState lastNonIdleMovingState, MovingState movingState)
        {
            ActionState = actionState;
            LastMovingState = lastMovingState;
            LastNonIdleMovingState = lastNonIdleMovingState;
            MovingState = movingState;
        }

        public PositionTransform GetMove()
        {
            return GetMove(MovingState);
        }

        public PositionTransform GetLastMove()
        {
            return GetMove(LastMovingState);
        }

        public PositionTransform GetLastNonIdleMove()
        {
            return GetMove(LastNonIdleMovingState);
        }
        public PositionTransform GetMove(MovingState state)
        {
            return state switch
            {
                MovingState.MovingUp => new PositionTransform(0, -1, DirectionType.Up),
                MovingState.MovingDown => new PositionTransform(0, 1, DirectionType.Down),
                MovingState.MovingLeft => new PositionTransform(-1, 0, DirectionType.Left),
                MovingState.MovingRight => new PositionTransform(1, 0, DirectionType.Right),
                MovingState.Idle => new PositionTransform(0, 0, DirectionType.None),
                _ => throw new Exception("Unknown player state")
            };
        }
    }
}
