﻿using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.Food;
using Nibbles.GameObject.Projectiles;
using System.Drawing;

namespace Nibbles.Engine
{
    public class NibblesStateHandler : IGameStateHandler
    {
        public event Action? GameOver;
        private readonly ISpriteRenderer _renderer;
        private readonly ICollisionDetector _collisionDetector;
        private readonly GameState _state;

        public NibblesStateHandler(GameState state, ISpriteRenderer renderer, ICollisionDetector collisionDetector)
        {
            _state = state;
            _renderer = renderer;
            _collisionDetector = collisionDetector;
            InitializeSprites();
        }

        private void InitializeSprites()
        {
            _renderer.AddRange(_state.Board.GetSprites());
            _renderer.AddRange(_state.Snake.GetSprites());
            _renderer.AddRange(_state.GameTitle.GetSprites());
            _renderer.AddRange(_state.Score.GetSprites());

            _collisionDetector.SnakeSelfCollision += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.SnakeVenomCollison += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.SnakeBoardCollison += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.VenomBoardCollision += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.VenomFoodCollision += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.SnakeFoodCollision += OnSnakeCollisionFood;

            RegisterSpriteEvents(_state.Snake);
            RegisterSpriteEvents(_state.GameOverTextBox);
            RegisterSpriteEvents(_state.Score);

            CreateFood();
        }

        public void PlayerMove()
        {
            _state.Score.IncrementMoves();
        }

        public void PlayerShoot()
        {
            if (_state.Venom != null) return;

            _state.Venom = _state.Snake.Shoot();
            _state.Venom.SpriteDestroyed += OnSpriteDestroyed;
            _state.Venom.SpriteCreated += OnSpriteCreated;
            _state.Venom.VenomDestroyed += OnVenomDestroyed;
        }

        public void UpdateState(PositionTransform playerInput, long timeDelta)
        {
            _collisionDetector.Detect();
            _state.Snake.Move(playerInput, timeDelta);
            _state.Venom?.Move(timeDelta);
        }

        private void OnSnakeCollisionFood()
        {
            _state.Snake.Feed();
            _state.Score.IncrementAmountEaten();
            CreateFood();
        }

        private void HandleGameOver(string text)
        {
            _state.GameOverTextBox.SetText(text);
            GameOver?.Invoke();
        }

        private void CreateFood()
        {
            var position = PositionGenerator.GetRandomPosition(
                new AbsolutePosition(new Point(0, 0), new Size(
                    SpriteConfig.BoardSizeX,
                    SpriteConfig.BoardSizeY)),
                _state.GetUnavailableFoodPositions());

            _state.Food = new FoodSprite(position);
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

        private void OnSpriteCreated(ISprite sprite)
        {
            _renderer.Add(sprite);
        }

        private void OnSpriteDestroyed(ISprite sprite)
        {
            _renderer.Remove(sprite);
        }

        private void RegisterSpriteEvents(ISprite sprite)
        {
            sprite.SpriteCreated += OnSpriteCreated;
            sprite.SpriteDestroyed += OnSpriteDestroyed;
        }
    }
}