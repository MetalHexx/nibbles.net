namespace Nibbles.GameObject.Dimensions
{
    public record PositionTransform(int XDelta, int YDelta, DirectionType Direction);

    public record MoveUp : PositionTransform
    {
        public MoveUp(int amount = 1) : base(0, amount * -1, DirectionType.Up) { }
    }

    public record MoveDown : PositionTransform
    {
        public MoveDown(int amount = 1) : base(0, amount, DirectionType.Down) { }
    }

    public record MoveLeft : PositionTransform
    {
        public MoveLeft(int amount = 1) : base(amount * -1, 0, DirectionType.Left) { }
    }

    public record MoveRight : PositionTransform
    {
        public MoveRight(int amount = 1) : base(amount, 0, DirectionType.Right) { }
    }
}
