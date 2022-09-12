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
            _gameState.GameOver += () => _continueGame = false;
            Render();
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
                Render();
            }
            while (_continueGame);
        }

        private void Render()
        {
            var spritesToRender = _gameState.GetSpritesToRender();            
            _renderer.RenderSprites(spritesToRender);
        }

        private void HandlePlayerMoveScore(PlayerInput playerInput)
        {
            if (playerInput.Type == PlayerInputType.Move)
            {
                _gameState.IncrementMoveScore();
            }
        }
    }
}
