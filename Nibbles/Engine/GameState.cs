using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Food;
using Nibbles.GameObject.Projectiles;
using Nibbles.GameObject.Snake;
using Nibbles.GameObject.UI;
using System.Drawing;

namespace Nibbles.Engine
{
    public class GameState
    {
        public FoodSprite? Food { get; set; }
        public SnakeContainer Snake { get; init; } = new();
        public Venom? Venom { get; set; }
        public GameTextBox GameOverTextBox { get; init; }

        public Board Board { get; init; } = new(
            new Point(0, 0), new Size(100, 20));

        public Score Score { get; init; } = new(
            new Point(SpriteConfig.GAME_TITLE.Length + 1, 0), "");

        public GameText GameTitle { get; init; } = new(
            new Point(1, 0),
            SpriteConfig.GAME_TITLE,
            SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR,
            SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR);

        public GameState()
        {
            GameOverTextBox = new GameTextBox("",
                new Point(Board.Size.Width / 2 - 8, Board.Size.Height / 2 - 2),
                new Size(16, 4));
        }
    }
}
