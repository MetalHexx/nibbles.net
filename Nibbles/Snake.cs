namespace Nibbles
{
    public class Snake: IMoveableGameObject
    {
        private List<SnakePart> _snakeParts = new List<SnakePart>() { new SnakePart(5, 5) };
        public GameObjectPosition Position => _snakeParts.First().Position;
        private GameObjectDirection _currentDirection = GameObjectDirection.Right;
        private int _feedCount = 0;

        public void Feed() => _feedCount = 5;

        /// <summary>
        /// Calculate and do the next snake move
        /// </summary>
        /// <param name="nextDirection">Next potential snake move based on users input (or lack of input)</param>
        public void Move(GameObjectDirection nextDirection)
        {
            var finalDirection = DetermineFinalDirection(nextDirection);

            switch (finalDirection)
            {
                case GameObjectDirection.Up:
                    DoMove(0, -1);
                    break;

                case GameObjectDirection.Down:
                    DoMove(0, 1);
                    break;

                case GameObjectDirection.Left:
                    DoMove(-1, 0);
                    break;

                case GameObjectDirection.Right:
                    DoMove(1, 0);
                    break;
            }
        }

        /// <summary>
        /// Prevents snake from moving in the opposite direction unless it's length is 1
        /// </summary>
        private GameObjectDirection DetermineFinalDirection(GameObjectDirection nextDirection) 
        {
            if (_snakeParts.Count() == 1) nextDirection = nextDirection;
            else if (_currentDirection == GameObjectDirection.Up && nextDirection == GameObjectDirection.Down) nextDirection = GameObjectDirection.NoChange;
            else if (_currentDirection == GameObjectDirection.Down && nextDirection == GameObjectDirection.Up) nextDirection =  GameObjectDirection.NoChange;
            else if (_currentDirection == GameObjectDirection.Left && nextDirection == GameObjectDirection.Right) nextDirection = GameObjectDirection.NoChange;
            else if (_currentDirection == GameObjectDirection.Right && nextDirection == GameObjectDirection.Left) nextDirection = GameObjectDirection.NoChange;

            if (nextDirection == GameObjectDirection.NoChange)
            {
                nextDirection = _currentDirection;
            }
            else
            {
                _currentDirection = nextDirection;
            }

            return nextDirection;
        }

        private void DoMove(int xDelta, int yDelta)
        {
            var head = _snakeParts.First();
            var newHead = Clone(head, xDelta, yDelta);
            _snakeParts.Insert(0, newHead);

            if (_feedCount > 0)
            {   
                _feedCount--;
            }
            else
            {
                _snakeParts.Remove(_snakeParts.Last());
            }
        }

        public IEnumerable<SnakePart> GetParts()
        {
            return _snakeParts.Select(sp => Clone(sp));
        }

        public bool TouchingSelf => GetParts()
            .Skip(1)
            .Any(snakePart => Position == snakePart.Position);

        /// <summary>
        /// Clone snake for immutability.  Only snake should move itself.
        /// </summary>
        public SnakePart Clone(SnakePart snakePart, int xDelta = 0, int yDelta = 0)
        {
            return snakePart with
            {
                Position = snakePart.Position with
                {
                    XPosition = snakePart.Position.XPosition + xDelta,
                    YPosition = snakePart.Position.YPosition + yDelta
                }
            };
        }
    }
}
