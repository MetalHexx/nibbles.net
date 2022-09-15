using Nibbles.GameObject.Dimensions;

namespace Nibbles.Player
{
    public class PlayerInput
    {
        public PlayerState MoveState { get; private set; } = PlayerState.MovingRight;
        public PlayerState PreviousMoveState { get; private set; } = PlayerState.Idle;

        public void UpdateState()
        {
            PreviousMoveState = MoveState;

            MoveState = (MoveState, GetPlayerInput()) switch
            {
                (PlayerState.MovingLeft, PlayerInputType.PressedUp) => PlayerState.MovingUp,
                (PlayerState.MovingLeft, PlayerInputType.PressedDown) => PlayerState.MovingDown,
                (PlayerState.MovingRight, PlayerInputType.PressedUp) => PlayerState.MovingUp,
                (PlayerState.MovingRight, PlayerInputType.PressedDown) => PlayerState.MovingDown,
                (PlayerState.MovingUp, PlayerInputType.PressedLeft) => PlayerState.MovingLeft,
                (PlayerState.MovingUp, PlayerInputType.PressedRight) => PlayerState.MovingRight,
                (PlayerState.MovingDown, PlayerInputType.PressedLeft) => PlayerState.MovingLeft,
                (PlayerState.MovingDown, PlayerInputType.PressedRight) => PlayerState.MovingRight,
                _ => MoveState
            };
        }

        public PositionTransform GetMove()
        {
            return MoveState switch
            {
                PlayerState.MovingUp => new PositionTransform(0, -1, DirectionType.Up),
                PlayerState.MovingDown => new PositionTransform(0, 1, DirectionType.Down),
                PlayerState.MovingLeft => new PositionTransform(-1, 0, DirectionType.Left),
                PlayerState.MovingRight => new PositionTransform(1, 0, DirectionType.Right),
                _ => throw new Exception("Unknown player state")
            };
        }

        private PlayerInputType GetPlayerInput()
        {
            return !Console.KeyAvailable 
                ? PlayerInputType.NoInput
                : Console.ReadKey(true).Key switch
                {
                    ConsoleKey.W => PlayerInputType.PressedUp,
                    ConsoleKey.UpArrow => PlayerInputType.PressedUp,
                    ConsoleKey.S => PlayerInputType.PressedDown,
                    ConsoleKey.DownArrow => PlayerInputType.PressedDown,
                    ConsoleKey.A => PlayerInputType.PressedLeft,
                    ConsoleKey.LeftArrow => PlayerInputType.PressedLeft,
                    ConsoleKey.D => PlayerInputType.PressedRight,
                    ConsoleKey.RightArrow => PlayerInputType.PressedRight,
                    _ => PlayerInputType.NoInput
                };
        }
    }
}
