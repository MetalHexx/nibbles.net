﻿namespace Nibbles
{
    public class GameState
    {
        public Snake Snake { get; private set; }
        public Food? Food { get; private set; }
        public GameBoard GameBoard { get; private set; } = new GameBoard();
        private PositionGenerator _positionGenerator = new PositionGenerator();
        public int TotalMoves { get; set; }
        public int AmountEaten = -1;      
        public int CurrentScore
        {
            get { return AmountEaten * _scorePerFeeding - (_penaltyPerMove * TotalMoves); }
        }

        private const int _scorePerFeeding = 100;
        private const int _penaltyPerMove = 1;

        public GameState()
        {
            Snake = new Snake();
            CreateFood();
        }

        public GameEvent DetermineGameEvents()
        {
            if (Snake.IsTouchingSelf || _gameBoardCollision)
            {
                Console.WriteLine("You lose! :(");
                return GameEvent.Lose;
            }
            if (Food is null)
            {
                Console.WriteLine("You win! :)");
                return GameEvent.Win;
            }
            return GameEvent.Continue;
        }

        public List<IGameObject?> GetGameObjects()
        {
            var gameObjects = Snake.GetParts()
                .Select(sp => sp as IGameObject)
                .ToList();
            gameObjects.Add(Food);
            return gameObjects;
        }

        public void CreateFood()
        {
            AmountEaten++;

            var positionsToAvoidFoodPlacement = Snake
                        .GetParts()
                        .Select(sp => sp.Position)
                        .ToArray();

            var foodPosition = _positionGenerator.GetRandomPositionWithoutOverlap(GameBoard.MaxX - 1, GameBoard.MaxY - 1, positionsToAvoidFoodPlacement);

            Food = new Food(foodPosition);
        }

        private bool _gameBoardCollision =>
            Snake.Position.XPosition == GameBoard.MinX
            ||
            Snake.Position.XPosition == GameBoard.MaxX
            ||
            Snake.Position.YPosition == GameBoard.MinY
            ||
            Snake.Position.YPosition == GameBoard.MaxY;
    }
}
