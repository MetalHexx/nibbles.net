using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.UI;
using System.Drawing;

namespace MainMenu
{
    public class MenuManager : GameManager
    {
        public Action<string>? GameSelected;
        private readonly ISpriteRenderer _renderer;
        private readonly IPlayer _player;
        private Menu _menu;

        public MenuManager(ISpriteRenderer renderer, IPlayer player) : base(renderer)
        {
            _renderer = renderer;
            _player = player;

            Console.Clear();

            var menuItems = new List<string> 
            { 
                GameMenuConfig.SNAKES, 
                GameMenuConfig.TETRIS, 
                GameMenuConfig.MINECRAFT_2D 
            };
            _menu = new(new Point(0, 0), new Size(30, 10), 0, "nibbles.net arcade", menuItems, GameColor.DarkYellow, GameColor.DarkMagenta);
            _menu.SpriteCreated += OnSpriteCreated;
            _renderer.Add(_menu);

        }

        public override void GenerateFrame()
        {
            var playerState = _player.NextState();
            var direction = playerState.GetMove().Direction;

            if(direction is DirectionType.Up or DirectionType.Down)
            {
                _menu.SelectNextMenuItem(playerState.GetMove().Direction);
            }

            if(playerState.ActionState == ActionState.Shooting)
            {
                GameOver?.Invoke();
                GameSelected?.Invoke(_menu.SelectedMenuItem.Text);                
            }
        }

        protected override void HandleGameOver(string text)
        {
            throw new NotImplementedException();
        }

        protected override void HandleGameWin(string text)
        {
            throw new NotImplementedException();
        }

        protected override void InitializeSprites()
        {
            throw new NotImplementedException();
        }
    }
}
