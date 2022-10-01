using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.UI;
using SnakesGame.GameObject;
using System.Drawing;

namespace SnakesGame.Engine
{
    public class SnakesState
    {
        public IPlayer Player { get; set; }
        public Food Food { get; set; }
        public SnakeContainer Snake { get; init; } = new();
        public Venom? Venom { get; set; }
        public GameTextBox GameOverTextBox { get; init; }

        public Board Board { get; init; } 

        public Score Score { get; init; } = new(
            new Point(SnakesConfig.GAME_TITLE.Length + 4, 1), GameConfig.BOARD_TEXT_ZINDEX, "");

        public GameText GameTitle { get; init; } = new(
            new Point(3, 1),
            GameConfig.BOARD_TEXT_ZINDEX,
            SnakesConfig.GAME_TITLE,
            DirectionType.None,
            SnakesConfig.BOARD_BORDER_FOREGROUND_COLOR,
            SnakesConfig.BOARD_BORDER_BACKGROUND_COLOR,
            0, 0);

        public SnakesState(IPlayer player, Size boardSize)
        {
            Player = player;

            Board = new(
                new Point(1, 1),
                new Size(boardSize.Width - 2, boardSize.Height - 1),
                GameConfig.BOARD_ZINDEX);

            Food = CreateFood();

            

            GameOverTextBox = new GameTextBox("",
                new Point(Board.Size.Width / 2 - 8, Board.Size.Height / 2 - 2),
                new Size(17, 5));
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

        public Food CreateFood()
        {
            var position = PositionGenerator.GetRandomPosition(
                new AbsolutePosition(new Point(3, 2), new Size(
                    SnakesConfig.BoardSizeX,
                    SnakesConfig.BoardSizeY)),
                GetUnavailableFoodPositions());

            Food = new Food(position);
            return Food;
        }
    }
}
