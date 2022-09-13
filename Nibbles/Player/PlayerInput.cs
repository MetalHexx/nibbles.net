using Nibbles.GameObject.Dimensions;

namespace Nibbles
{
    public class PlayerInput
    {
        public PlayerInputType Type { get; private set; }
        private ConsoleKey? _inputValue;
        public PositionTransform Transform { get; private set; }
        

        public PlayerInput()
        {
            ReadKeyboardInput();            
            Transform = GetPositionTransform();
            SetType();
        }

        public static PlayerInput Get() => new PlayerInput();

        private void ReadKeyboardInput()
        {
            if (!Console.KeyAvailable)
            {
                Type = PlayerInputType.None;
                return;
            }
            _inputValue = Console.ReadKey(true).Key;
        }

        private void SetType()
        {
            Type = Transform.Direction == DirectionType.NoChange
                            ? PlayerInputType.None
                            : PlayerInputType.Move;
        }

        private PositionTransform GetPositionTransform()
        {
            switch (_inputValue)
            {
                case ConsoleKey.W:
                    return new PositionTransform(0, -1, DirectionType.Up);

                case ConsoleKey.UpArrow:
                    return new PositionTransform(0, -1, DirectionType.Up);

                case ConsoleKey.S:
                    return new PositionTransform(0, 1, DirectionType.Down);

                case ConsoleKey.DownArrow:
                    return new PositionTransform(0, 1, DirectionType.Down);

                case ConsoleKey.A:
                    return new PositionTransform(-1, 0, DirectionType.Left);

                case ConsoleKey.LeftArrow:
                    return new PositionTransform(-1, 0, DirectionType.Left);

                case ConsoleKey.D:
                    return new PositionTransform(1, 0, DirectionType.Right);

                case ConsoleKey.RightArrow:
                    return new PositionTransform(1, 0, DirectionType.Right);

                default:
                    return new PositionTransform(0, 0, DirectionType.NoChange);
            }
        }
    }
}
