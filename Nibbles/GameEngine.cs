using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles
{
    internal class GameEngine
    {
        private SnakerRenderer _snakeRenderer = new SnakerRenderer();
        private FoodGenerator _foodGenerator = new FoodGenerator();
        private SnakeInputHandler _snakeInputHandler = new SnakeInputHandler();
        private GameState _gameState;

        public GameEngine()
        {   
            var snake = new Snake();
            var food = NextFood(snake); 
            _gameState = new GameState(snake, food);
        }
        public void Start()
        {
            var currentState = GameStateOutcome.Continue;

            while (currentState == GameStateOutcome.Continue)
            {
                RenderSnake();
                Thread.Sleep(100);

                var directionChange = _snakeInputHandler.GetDirection();

                if (_gameState.Snake.Position == _gameState.Food?.Position)
                {
                    _gameState.Snake.Feed();
                    _gameState.Food = NextFood(_gameState.Snake);
                }
                ClearSnake();
                _gameState.Snake.MoveSnake(directionChange);
                currentState = _gameState.DetermineState();
            }
        }

        private void RenderSnake() => _snakeRenderer.Render(_gameState.Snake.GetParts());
        private void ClearSnake() => _snakeRenderer.Clear(_gameState.Snake.GetParts());
        private Food? NextFood(Snake snake) => _foodGenerator.Next(snake
            .GetParts()
            .Select(p => p.Position));
    }
}
