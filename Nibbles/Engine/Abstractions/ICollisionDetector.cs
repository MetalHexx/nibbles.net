namespace Nibbles.Engine.Abstractions
{
    public interface ICollisionDetector
    {
        Action? SnakeFoodCollision { get; set; }
        Action? SnakeSelfCollision { get; set; }
        Action? SnakeVenomCollison { get; set; }
        Action? SnakeBoardCollison { get; set; }
        Action? VenomBoardCollision { get; set; }
        Action? VenomFoodCollision { get; set; }
        void Detect();
    }
}
