namespace Nibbles
{
    internal class GameEngine
    {
        private SnakeInputHandler _snakeInputHandler = new SnakeInputHandler();
        private GameObjectRenderer _renderer = new GameObjectRenderer();
        private GameState _gameState;

        public GameEngine()
        {
            _gameState = new GameState();
            _renderer.RenderBoard(_gameState.GameBoard);
        }
        public void Start()
        {
            var currentEvent = GameEvent.Continue;

            while (currentEvent == GameEvent.Continue)
            {
                _renderer.Render(_gameState.GetGameObjects());
                Thread.Sleep(100);

                var directionChange = _snakeInputHandler.GetDirection();
                MaybeFeedSnakeAndCreateFood();
                _renderer.Clear(_gameState.GetGameObjects());
                _gameState.Snake.MoveSnake(directionChange);
                currentEvent = _gameState.DetermineGameEvents();
            }
        }

        private void MaybeFeedSnakeAndCreateFood()
        {
            if (_gameState.Snake.Position == _gameState.Food?.Position)
            {
                _gameState.Snake.Feed();
                _gameState.CreateFood();
            }
        }               
    }
}
