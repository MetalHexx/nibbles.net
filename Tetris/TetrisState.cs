using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.UI;
using System.Drawing;
using Tetris.GameObjects;

namespace Tetris
{
    internal class TetrisState
    {
        public IPlayer Player { get; set; }
        public Board Board { get; init; }
        public GameTextBox GameOverTextBox { get; init; }
        public Tetrimino Tetrimino { get; private set; }

        public Score Score { get; init; } = new(
            new Point(TetrisConfig.GAME_TITLE.Length + 4, 1), GameConfig.BOARD_TEXT_ZINDEX, "");

        public GameText GameTitle { get; init; } = new(
            new Point(3, 1),
            GameConfig.BOARD_TEXT_ZINDEX,
            TetrisConfig.GAME_TITLE,
            DirectionType.None,
            TetrisConfig.BOARD_BORDER_FOREGROUND_COLOR,
            TetrisConfig.BOARD_BORDER_BACKGROUND_COLOR,
            0, 0);

        public TetrisState(Player player, Size boardSize)
        {
            Player = player;

            Board = new(
                new Point(1, 1),
                new Size(boardSize.Width - 2, boardSize.Height - 1),
                GameConfig.BOARD_ZINDEX);

            GameOverTextBox = new GameTextBox("",
                    new Point(Board.Size.Width / 2 - 8, Board.Size.Height / 2 - 2),
                    new Size(17, 5));

            CreateTetrimino();
        }

        public Tetrimino CreateTetrimino()
        {
            Tetrimino = (new Random().Next(7)) switch
            {
                0 => new IShapedTetrimino(),
                1 => new JShapedTetrimino(),
                2 => new LShapedTetrimino(),
                3 => new OShapedTetrimino(),
                4 => new SShapedTetrimino(),
                5 => new ZShapedTetrimino(),
                6 => new TShapedTetrimino(),
                _ => throw new ArgumentOutOfRangeException("Invalid tetrino value")
            };
            return Tetrimino;
        }
    }
}