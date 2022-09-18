using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine.Abstractions
{
    public interface IGameStateHandler
    {
        event Action? FoodEaten;
        event Action? GameOver;

        void CheckGameBoardCollision();
        void CreateFood();
        void DetectFoodCollision();
        void FeedSnake();
        void IncrementMoveScore();
        void UpdateSprites(PositionTransform transform, long renderDelta);
        void SnakeShoot();
    }
}