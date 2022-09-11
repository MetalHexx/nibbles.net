namespace Nibbles.Engine
{
    internal class GameEngine
    {
        private GameRenderer _renderer = new GameRenderer();
        private GameState _gameState;

        public GameEngine()
        {
            _gameState = new GameState();
            _renderer.RenderBoard(_gameState.GameBoard);
            _renderer.RenderScore(_gameState);
        }
        public void Start()
        {
            var currentEvent = GameEvent.Continue;

            while (currentEvent == GameEvent.Continue)
            {
                _renderer.Render(_gameState.GetGameObjects());
                Thread.Sleep(100);

                var playerInput = PlayerInput.Get();
                HandleScore(playerInput);
                HandleFoodCollision();
                ClearGameObjects();
                MoveSnake(playerInput);
                currentEvent = _gameState.DetermineGameEvents();
            }
        }

        private void MoveSnake(PlayerInput playerInput)
        {
            _gameState.Snake.Move(playerInput.Transform);
        }

        private void HandleScore(PlayerInput playerInput)
        {
            if (playerInput.Type == PlayerInputType.Move)
            {
                _gameState.Score.IncrementMoves();
            }
        }

        private void HandleFoodCollision()
        {
            if (_gameState.Snake.Position == _gameState.Food?.Position)
            {
                _gameState.Snake.Feed();
                _gameState.Score.IncrementAmountEaten();
                _gameState.CreateFood();
                _renderer.RenderScore(_gameState);
            }
        }

        private void ClearGameObjects()
        {
            var gameObjects = _gameState.GetGameObjects();
            _renderer.Clear(gameObjects);
        }
    }
}
