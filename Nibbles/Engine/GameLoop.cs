﻿using Nibbles.Engine.Abstractions;
using Nibbles.Player;

namespace Nibbles.Engine
{
    public class GameLoop
    {
        private readonly ISpriteRenderer _renderer;
        private readonly IGame _actions;
        private readonly IPlayerInput _player;
        private bool _continueGame = true;
        private long _lastRenderTicks = DateTime.Now.Ticks; 

        public GameLoop(IPlayerInput player, ISpriteRenderer renderer, IGame actions)
        {
            _player = player;
            _renderer = renderer;
            _actions = actions;
            _actions.GameOver += () => _continueGame = false;
            _player.Moved += _actions.PlayerMove;
            _player.Shot += _actions.PlayerShoot;
            _renderer.Render();            
        }
        public void Start()
        {
            do
            {   
                _player.UpdateState();
                _actions.UpdateState(_player.GetMove(), GetTimeSinceRender());
                _renderer.Render();
            }
            while (_continueGame);
        }

        public long GetTimeSinceRender()
        {
            var currentTick = DateTime.Now.Ticks;
            var delta = currentTick - _lastRenderTicks;
            _lastRenderTicks = currentTick;
            return delta;
        }
    }
}
