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
        private Board _board;
        private GameText _gameTitle;

        public MenuManager(ISpriteRenderer renderer, IPlayer player, Size boardSize) : base(renderer)
        {
            _renderer = renderer;
            _player = player;

            _gameTitle = new(
                new Point(3, 1),
                GameConfig.BOARD_TEXT_ZINDEX,
                GameMenuConfig.BOARD_TITLE,
                DirectionType.None,
                GameMenuConfig.BOARD_BORDER_FOREGROUND_COLOR,
                GameMenuConfig.BOARD_BORDER_BACKGROUND_COLOR,
                0, 0);

            _board = new(
                new Point(1, 1),
                new Size(boardSize.Width - 2, boardSize.Height - 1),
                GameConfig.BOARD_ZINDEX);

            var menuItems = new List<string>
            {
                GameMenuConfig.SNAKES_TITLE,
                GameMenuConfig.TETRIS_TITLE,
                GameMenuConfig.MINECRAFT_2D_TITLE
            };

            var menuXPosition = _board.Size.Width / 2 - 15;
            var menuYPosition = _board.Size.Height / 2 - 5;

            _menu = new(
                new Point(menuXPosition, menuYPosition), 
                new Size(30, 10), GameMenuConfig.MENU_ZINDEX, 
                GameMenuConfig.MENU_TITLE, 
                menuItems, 
                GameColor.Yellow, 
                GameColor.DarkMagenta);
            
            _menu.SpriteCreated += OnSpriteCreated;

            _renderer.Add(_menu);
            _renderer.Add(_board);
            _renderer.Add(_gameTitle);
            _renderer.Add(_board);

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
                GameSelected?.Invoke(_menu.SelectedMenuItem.Name);                
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
