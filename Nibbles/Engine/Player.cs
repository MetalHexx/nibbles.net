using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine
{
    public class Player : IPlayer
    {
        private readonly IInputReader _inputReader;
        public MovingState MovingState { get; private set; } = MovingState.MovingRight;
        public MovingState LastMovingState { get; private set; } = MovingState.MovingRight;
        public MovingState LastNonIdleMovingState { get; private set; } = MovingState.MovingRight;
        public ActionState ActionState { get; private set; } = ActionState.Idle;

        public Player(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public PlayerState NextState()
        {
            var LastMovingState = MovingState;
            
            var playerInput = _inputReader.Read();

            MovingState = (LastMovingState, playerInput) switch
            {
                (MovingState.MovingLeft, InputType.Up) => MovingState.MovingUp,
                (MovingState.MovingLeft, InputType.Down) => MovingState.MovingDown,
                (MovingState.MovingRight, InputType.Up) => MovingState.MovingUp,
                (MovingState.MovingRight, InputType.Down) => MovingState.MovingDown,
                (MovingState.MovingUp, InputType.Left) => MovingState.MovingLeft,
                (MovingState.MovingUp, InputType.Right) => MovingState.MovingRight,
                (MovingState.MovingDown, InputType.Left) => MovingState.MovingLeft,
                (MovingState.MovingDown, InputType.Right) => MovingState.MovingRight,
                (MovingState.Idle, InputType.Up) => MovingState.MovingUp,
                (MovingState.Idle, InputType.Down) => MovingState.MovingDown,
                (MovingState.Idle, InputType.Left) => MovingState.MovingLeft,
                (MovingState.Idle, InputType.Right) => MovingState.MovingRight,
                _ => MovingState.Idle
            };

            ActionState = (ActionState, playerInput) switch
            {
                (ActionState.Idle, InputType.Spacebar) => ActionState.Shooting,
                _ => ActionState.Idle
            };

            LastNonIdleMovingState = MovingState is MovingState.Idle
                ? LastNonIdleMovingState
                : MovingState;

            return new PlayerState(ActionState, LastMovingState, LastNonIdleMovingState, MovingState);
        }
    }
}
