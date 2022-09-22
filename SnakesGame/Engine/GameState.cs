using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.UI;
using SnakesGame.GameObject;
using System.Drawing;

namespace SnakesGame.Engine
{
    public class GameState
    {
        public FoodSprite Food { get; set; }
        public SnakeContainer Snake { get; init; } = new();
        public Venom? Venom { get; set; }
        public GameTextBox GameOverTextBox { get; init; }

        public Board Board { get; init; } = new(
            new Point(0, 0), new Size(SnakesConfig.BoardSizeX, SnakesConfig.BoardSizeY));

        public Score Score { get; init; } = new(
            new Point(SnakesConfig.GAME_TITLE.Length + 1, 0), "");

        public GameText GameTitle { get; init; } = new(
            new Point(1, 0),
            SnakesConfig.GAME_TITLE,
            DirectionType.None,
            SnakesConfig.BOARD_BORDER_FOREGROUND_COLOR,
            SnakesConfig.BOARD_BORDER_BACKGROUND_COLOR,
            0, 0);

        public GameState()
        {
            GameOverTextBox = new GameTextBox("",
                new Point(Board.Size.Width / 2 - 8, Board.Size.Height / 2 - 2),
                new Size(16, 4));

            CreateFood();
        }

        public IList<ISprite> GetUnavailableFoodPositions()
        {
            var sprites = new List<ISprite>();

            if (Food != null) sprites.Add(Food);
            if (Venom != null) sprites.Add(Venom);

            sprites.AddRange(Snake.GetSprites());
            sprites.AddRange(Board.GetSprites()
                .Where(s => s is BorderPart));

            return sprites;
        }

        public FoodSprite CreateFood()
        {
            var position = PositionGenerator.GetRandomPosition(
                new AbsolutePosition(new Point(0, 0), new Size(
                    SnakesConfig.BoardSizeX,
                    SnakesConfig.BoardSizeY)),
                GetUnavailableFoodPositions());

            Food = new FoodSprite(position);
            return Food;
        }
    }
}
