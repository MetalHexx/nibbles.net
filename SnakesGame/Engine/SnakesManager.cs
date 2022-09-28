﻿using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using SnakesGame.GameObject;

namespace SnakesGame.Engine
{
    public class SnakesManager : GameManager
    {
        private readonly ICollisionDetector _collisionDetector;
        private readonly ITopScoreStore _scoreStore;
        private readonly GameState _state;

        public SnakesManager(GameState state, ISpriteRenderer renderer, ICollisionDetector collisionDetector, ITopScoreStore scoreStore) : base(renderer)
        {
            _state = state;
            _collisionDetector = collisionDetector;
            _scoreStore = scoreStore;
            InitializeSprites();
        }

        protected override void InitializeSprites()
        {               
            _renderer.Add(_state.GameTitle);
            _renderer.Add(_state.Score);
            _renderer.Add(_state.Snake);
            _renderer.Add(_state.Food);
            _renderer.Add(_state.Board);

            _collisionDetector.SnakeSelfCollision += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.SnakeVenomCollison += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.SnakeBoardCollison += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.VenomBoardCollision += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.VenomFoodCollision += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.SnakeFoodCollision += OnSnakeCollisionFood;

            RegisterEvents(_state.Snake);
            RegisterEvents(_state.GameOverTextBox);
            RegisterEvents(_state.Score);
            RegisterEvents(_state.Board);
        }

        public void ShowTopScores()
        {
            var scores = _scoreStore.GetScores(SnakesConfig.GAME_ID);            
            Console.ReadLine();
        }

        public override void GenerateFrame()
        {
            var timeSinceLastFrame = GetTimeSinceLastFrame();
            var playerState = _state.Player.NextState();
            HandlePlayerMove(timeSinceLastFrame, playerState);
            HandlePlayerShoot(timeSinceLastFrame, playerState);
            _collisionDetector.Detect();
        }

        private void HandlePlayerMove(long timeSinceLastFrame, PlayerState playerState)
        {
            if (playerState.MovingState == MovingState.Idle)
            {
                _state.Snake.Move(playerState.GetLastNonIdleMove(), timeSinceLastFrame);
            }
            else
            {
                _state.Score.IncrementMoves();
                _state.Snake.Move(playerState.GetMove(), timeSinceLastFrame);
            }
        }

        private void HandlePlayerShoot(long timeSinceLastFrame, PlayerState playerState)
        {
            var snakeShouldShoot = playerState.ActionState == ActionState.Shooting 
                && _state.Venom is null;

            if (snakeShouldShoot)
            {
                    _state.Venom = _state.Snake.Shoot();
                    _state.Venom.SpriteDestroyed += OnSpriteDestroyed;
                    _state.Venom.SpriteCreated += OnSpriteCreated;
                    _state.Venom.VenomDestroyed += OnVenomDestroyed;
            }
            _state.Venom?.Move(timeSinceLastFrame);
        }

        protected override void HandleGameWin(string text)
        {
            throw new NotImplementedException(); //lol, no way to win yet
        }

        protected override void HandleGameOver(string text)
        {
            _state.GameOverTextBox.SetText(text);
            _scoreStore.SaveScore(new TopScore(SnakesConfig.GAME_ID, "HEX", _state.Score.Total));
            GameOver?.Invoke();
        }

        private void OnSnakeCollisionFood()
        {
            _state.Snake.Feed();
            _state.Score.IncrementAmountEaten();
            _state.CreateFood();
            _renderer.Add(_state.Food);            
        }

        private void OnVenomDestroyed(Venom venom)
        {
            _renderer.Remove(venom);

            if (_state.Venom == null) return;
            _state.Venom.SpriteDestroyed -= OnSpriteDestroyed;
            _state.Venom.SpriteCreated -= OnSpriteCreated;
            _state.Venom.VenomDestroyed -= OnVenomDestroyed;
            _state.Venom = null;
        }
    }
}
