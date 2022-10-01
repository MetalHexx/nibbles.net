using Nibbles.Engine;
using SnakesGame.Engine;
using System.Drawing;
using Tetris;

namespace MainMenu
{
    public class MenuManager
    {
        private readonly SnakesManager _snakes;        
        private readonly TetrisManager _tetrisManager;
        public MenuManager()
        {
            _snakes = new SnakesManager();
            _tetrisManager = new TetrisManager();
        }
        public void Start()
        {
            while (true)
            {
                CreateMenu().Start();
            }
        }

        public GameMenuLoop CreateMenu()
        {
            var inputReader = new KeyboardReader();
            var player = new Player(inputReader);
            var renderer = new SpriteRenderer();
            var mainMenu = new MenuStateReducer(renderer, player, new Size(Console.WindowWidth, Console.WindowHeight));
            mainMenu.GameSelected += ExecuteGame;
            return new GameMenuLoop(renderer, mainMenu);
        }

        private void ExecuteGame(string game)
        {
            Console.Clear();
            switch (game)
            {
                case GameMenuConfig.SNAKES_TITLE:
                    _snakes.Start();
                    break;

                case GameMenuConfig.TETRIS_TITLE:
                    _tetrisManager.Start();
                    break;

                default: return;
            }
        }
    }
}
