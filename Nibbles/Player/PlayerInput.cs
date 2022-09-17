using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.Player
{
    public class PlayerInput : IPlayerInput
    {
        private readonly IInputReader _inputReader;

        public event Action? Moved, Shot;
        public MovingState MoveState { get; private set; } = MovingState.MovingRight;
        public ActionState ActionState { get; private set; } = ActionState.Idle;

        public PlayerInput(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public void UpdateState()
        {
            var previousMoveState = MoveState;
            var playerInput = _inputReader.Get();

            MoveState = (MoveState, playerInput) switch
            {
                (MovingState.MovingLeft, InputType.Up) => MovingState.MovingUp,
                (MovingState.MovingLeft, InputType.Down) => MovingState.MovingDown,
                (MovingState.MovingRight, InputType.Up) => MovingState.MovingUp,
                (MovingState.MovingRight, InputType.Down) => MovingState.MovingDown,
                (MovingState.MovingUp, InputType.Left) => MovingState.MovingLeft,
                (MovingState.MovingUp, InputType.Right) => MovingState.MovingRight,
                (MovingState.MovingDown, InputType.Left) => MovingState.MovingLeft,
                (MovingState.MovingDown, InputType.Right) => MovingState.MovingRight,
                _ => MoveState
            };

            ActionState = (ActionState, playerInput) switch
            {
                (ActionState.Idle, InputType.Spacebar) => ActionState.Shooting,
                _ => ActionState.Idle
            };

            if (previousMoveState != MoveState) Moved?.Invoke();
            if (ActionState == ActionState.Shooting) Shot?.Invoke();
        }

        public PositionTransform GetMove()
        {
            return MoveState switch
            {
                MovingState.MovingUp => new PositionTransform(0, -1, DirectionType.Up),
                MovingState.MovingDown => new PositionTransform(0, 1, DirectionType.Down),
                MovingState.MovingLeft => new PositionTransform(-1, 0, DirectionType.Left),
                MovingState.MovingRight => new PositionTransform(1, 0, DirectionType.Right),
                _ => throw new Exception("Unknown player state")
            };
        }
    }
}
