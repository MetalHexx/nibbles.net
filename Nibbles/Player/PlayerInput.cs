using Nibbles.GameObject.Dimensions;

namespace Nibbles.Player
{
    public class PlayerInput
    {
        public event Action? Moved;
        public event Action? Shot;
        public PlayerMovingState MoveState { get; private set; } = PlayerMovingState.MovingRight;
        public PlayerActionState ActionState { get; private set; } = PlayerActionState.Idle;

        public void UpdateState()
        {
            var previousMoveState = MoveState;
            var playerInput = GetPlayerInput();

            MoveState = (MoveState, playerInput) switch
            {
                (PlayerMovingState.MovingLeft, PlayerInputType.Up) => PlayerMovingState.MovingUp,
                (PlayerMovingState.MovingLeft, PlayerInputType.Down) => PlayerMovingState.MovingDown,
                (PlayerMovingState.MovingRight, PlayerInputType.Up) => PlayerMovingState.MovingUp,
                (PlayerMovingState.MovingRight, PlayerInputType.Down) => PlayerMovingState.MovingDown,
                (PlayerMovingState.MovingUp, PlayerInputType.Left) => PlayerMovingState.MovingLeft,
                (PlayerMovingState.MovingUp, PlayerInputType.Right) => PlayerMovingState.MovingRight,
                (PlayerMovingState.MovingDown, PlayerInputType.Left) => PlayerMovingState.MovingLeft,
                (PlayerMovingState.MovingDown, PlayerInputType.Right) => PlayerMovingState.MovingRight,
                _ => MoveState
            };

            ActionState = (ActionState, playerInput) switch
            {
                (PlayerActionState.Idle, PlayerInputType.Spacebar) => PlayerActionState.Shooting,
                _ => PlayerActionState.Idle
            };

            if (previousMoveState != MoveState) Moved?.Invoke();
            if (ActionState == PlayerActionState.Shooting) Shot?.Invoke();
        }

        public PositionTransform GetMove()
        {
            return MoveState switch
            {
                PlayerMovingState.MovingUp => new PositionTransform(0, -1, DirectionType.Up),
                PlayerMovingState.MovingDown => new PositionTransform(0, 1, DirectionType.Down),
                PlayerMovingState.MovingLeft => new PositionTransform(-1, 0, DirectionType.Left),
                PlayerMovingState.MovingRight => new PositionTransform(1, 0, DirectionType.Right),
                _ => throw new Exception("Unknown player state")
            };
        }

        private PlayerInputType GetPlayerInput()
        {
            return !Console.KeyAvailable 
                ? PlayerInputType.NoInput
                : Console.ReadKey(true).Key switch
                {
                    ConsoleKey.W => PlayerInputType.Up,
                    ConsoleKey.UpArrow => PlayerInputType.Up,
                    ConsoleKey.S => PlayerInputType.Down,
                    ConsoleKey.DownArrow => PlayerInputType.Down,
                    ConsoleKey.A => PlayerInputType.Left,
                    ConsoleKey.LeftArrow => PlayerInputType.Left,
                    ConsoleKey.D => PlayerInputType.Right,
                    ConsoleKey.RightArrow => PlayerInputType.Right,
                    ConsoleKey.Spacebar => PlayerInputType.Spacebar,                    
                    _ => PlayerInputType.NoInput
                };
        }
    }
}
