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
        SpriteRenderUpdate GetSpritesToRender();
        void IncrementMoveScore();
        void MoveSnake(PositionTransform transform);
        void SnakeShoot();
    }
}