namespace Nibbles.Engine
{
    internal class GameEngine
    {
        private ISpriteRenderer _renderer = new SpriteRenderer();
        private GameState _gameState;
        private bool _continueGame = true;

        public GameEngine()
        {
            _gameState = new GameState();
            _gameState.GameLost += OnGameLost;
            _gameState.GameWon += OnGameWin;
            _gameState.FoodEaten += OnFoodEaten;
            _renderer.RenderBoard(_gameState.GameBoard);
            _renderer.RenderScore(_gameState);
            RenderSprites();
        }
        public void Start()
        {
            do
            {
                Thread.Sleep(100);
                var playerInput = PlayerInput.Get();
                HandlePlayerMoveScore(playerInput);                
                _gameState.DetectFoodCollision();
                _gameState.MoveSnake(playerInput.Transform);
                _gameState.CheckGameBoardCollision();
                DestroySprites();
                RenderSprites();
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

        private void OnFoodEaten()
        {
            _renderer.RenderScore(_gameState);
        }

        private void RenderSprites()
        {
            var spritesToRender = _gameState.GetSpritesToRender();            
            _renderer.Render(spritesToRender);
        }
        private void DestroySprites()
        {
            var spritesToDestroy = _gameState.GetSpritesToDestroy();
            _renderer.Destroy(spritesToDestroy);
        }

        private void HandlePlayerMoveScore(PlayerInput playerInput)
        {
            if (playerInput.Type == PlayerInputType.Move)
            {
                _gameState.IncrementMoves();
            }
        }
    }
}
