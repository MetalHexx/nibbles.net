namespace Nibbles
{
    public class Snake: IGameObject
    {
        private List<SnakePart> _snakeParts = new List<SnakePart>() { new SnakePart(5, 5) };
        public GameObjectPosition Position => _snakeParts.First().Position;
        private SnakeDirection _currentDirection = SnakeDirection.Right;
        private int _feedCount = 0;

        public void Feed() => _feedCount = 5;

        /// <summary>
        /// Calculate and do the next snake move
        /// </summary>
        /// <param name="nextDirection">Next potential snake move based on users input (or lack of input)</param>
        public void MoveSnake(SnakeDirection nextDirection)
        {
            var finalDirection = DetermineFinalDirection(nextDirection);

            switch (finalDirection)
            {
                case SnakeDirection.Up:
                    DoMove(0, -1);
                    break;

                case SnakeDirection.Down:
                    DoMove(0, 1);
                    break;

                case SnakeDirection.Left:
                    DoMove(-1, 0);
                    break;

                case SnakeDirection.Right:
                    DoMove(1, 0);
                    break;
            }
        }

        /// <summary>
        /// Prevents snake from moving in the opposite direction unless it's length is 1
        /// </summary>
        private SnakeDirection DetermineFinalDirection(SnakeDirection nextDirection) 
        {
            if (_snakeParts.Count() == 1) nextDirection = nextDirection;
            else if (_currentDirection == SnakeDirection.Up && nextDirection == SnakeDirection.Down) nextDirection = SnakeDirection.NoChange;
            else if (_currentDirection == SnakeDirection.Down && nextDirection == SnakeDirection.Up) nextDirection =  SnakeDirection.NoChange;
            else if (_currentDirection == SnakeDirection.Left && nextDirection == SnakeDirection.Right) nextDirection = SnakeDirection.NoChange;
            else if (_currentDirection == SnakeDirection.Right && nextDirection == SnakeDirection.Left) nextDirection = SnakeDirection.NoChange;

            if (nextDirection == SnakeDirection.NoChange)
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
