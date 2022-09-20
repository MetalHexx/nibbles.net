using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.UI;

namespace Nibbles.Engine
{
    public class CollisionDetector : ICollisionDetector
    {
        public Action? SnakeFoodCollision { get; set; }
        public Action? SnakeSelfCollision { get; set; }
        public Action? SnakeVenomCollison { get; set; }
        public Action? SnakeBoardCollison { get; set; }
        public Action? VenomBoardCollision { get; set; }
        public Action? VenomFoodCollision { get; set; }

        private readonly GameState _state;

        public CollisionDetector(GameState state) => _state = state;

        public void Detect()
        {
            var snakeSprites = _state.Snake.GetSprites();
            var gameBoarders = GetGameBoarders();

            DetectSnakeFoodCollision();
            DetectVenomFoodCollision();
            DetectSnakeVenomCollision(snakeSprites);
            DetectSnakeSelfCollision(snakeSprites);
            DetectSnakeBoardCollision(gameBoarders);
            DetectVenomBoardCollision(gameBoarders);
        }

        private IList<ISprite> GetGameBoarders()
        {
            var borders = _state.Board.GetSprites().ToList();
            borders.AddRange(_state.GameTitle.GetSprites());
            borders.AddRange(_state.Score.GetSprites());
            return borders;
        }

        private void DetectSnakeSelfCollision(IEnumerable<ISprite> snakeSprites)
        {
            var snakeCollideSelf = snakeSprites
                .Skip(1)
                .Any(snakeParts => snakeParts.Position == snakeSprites.First().Position);

            if (snakeCollideSelf) SnakeSelfCollision?.Invoke();
        }

        private void DetectSnakeVenomCollision(IEnumerable<ISprite> snakeSprites)
        {
            var snakeVenomCollision = snakeSprites
                .Any(snakePart => snakePart.Position == _state.Venom?.Position);

            if (snakeVenomCollision) SnakeVenomCollison?.Invoke();
        }

        private void DetectSnakeFoodCollision()
        {
            if (_state.Food?.Position == _state.Snake.Position)
            {
                SnakeFoodCollision?.Invoke();
            }
        }

        private void DetectVenomFoodCollision()
        {
            if (_state.Venom?.Position == _state.Food?.Position)
            {
                VenomFoodCollision?.Invoke();
            }
        }

        private void DetectSnakeBoardCollision(IList<ISprite> gameBoarders)
        {
            var snakeHead = _state.Snake.GetSprites().First();

            if (DetectSpriteBoardCollision(snakeHead, gameBoarders))
            {
                SnakeBoardCollison?.Invoke();
            }
        }

        private void DetectVenomBoardCollision(IList<ISprite> gameBoarders)
        {
            if (_state.Venom is null) return;

            var snakeBoardCollided = DetectSpriteBoardCollision(_state.Venom, gameBoarders);

            if (snakeBoardCollided) SnakeBoardCollison?.Invoke();
        }

        private bool DetectSpriteBoardCollision(ISprite sprite, IList<ISprite> gameBoarders) => gameBoarders
            .Where(sprite => sprite is BorderPart)
            .Any(border => border.Position == sprite.Position);

    }
}
