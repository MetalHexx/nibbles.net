namespace Nibbles.Engine
{
    internal class GameEngine
    {
        private GameRenderer _renderer = new GameRenderer();
        private GameState _gameState;
        private bool _continueGame = true;

        public GameEngine()
        {
            _gameState = new GameState();
            _gameState.GameLost += OnGameLost;
            _gameState.GameWon += OnGameWin;
            _renderer.RenderBoard(_gameState.GameBoard);
            _renderer.RenderScore(_gameState);
        }
        public void Start()
        {
            do
            {
                Render();
                Thread.Sleep(100);
                var playerInput = PlayerInput.Get();
                HandlePlayerMoveScore(playerInput);
                HandleFoodCollision();
                ClearGameObjects();
                MoveSnake(playerInput);
                CheckGameBoardCollision();
            } 
            while (_continueGame);
        }

        private void OnGameWin()
        {
            Console.WriteLine("You win! :)");
            _continueGame = false;
        }

        private void OnGameLost()
        {   
            Console.WriteLine("You lose! :(");
            _continueGame = false;
        }

        private void Render()
        {
            var gameObjects = _gameState.GetGameObjects();
            _renderer.Render(gameObjects);
        }

        private void MoveSnake(PlayerInput playerInput)
        {
            _gameState.Snake.Move(playerInput.Transform);
        }

        private void HandlePlayerMoveScore(PlayerInput playerInput)
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

        private void CheckGameBoardCollision() => _gameState.CheckGameBoardCollision();

        private void ClearGameObjects()
        {
            var gameObjects = _gameState.GetGameObjects();
            _renderer.Clear(gameObjects);
        }
    }
}
