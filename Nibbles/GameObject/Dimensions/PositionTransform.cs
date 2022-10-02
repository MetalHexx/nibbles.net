namespace Nibbles.GameObject.Dimensions
{
    public record PositionTransform(int XDelta, int YDelta, DirectionType Direction);

    public record MoveUp : PositionTransform
    {
        public MoveUp(int amount) : base(0, amount, DirectionType.Up) { }
    }

    public record MoveDown : PositionTransform
    {
        public MoveDown(int amount) : base(0, amount, DirectionType.Down) { }
    }

    public record MoveLeft : PositionTransform
    {
        public MoveLeft(int amount) : base(amount, 0, DirectionType.Left) { }
    }

    public record MoveRight : PositionTransform
    {
        public MoveRight(int amount) : base(amount, 0, DirectionType.Right) { }
    }
}
