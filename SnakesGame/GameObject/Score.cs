using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.UI;
using System.Drawing;

namespace SnakesGame.GameObject
{
    public class Score : GameText
    {
        public int Moves { get; set; }
        public int AmountEaten = 0;

        public int Total
        {
            get { return AmountEaten * _scorePerFeeding - _penaltyPerMove * Moves; }
        }

        private const int _scorePerFeeding = 100;
        private const int _penaltyPerMove = 1;

        public Score(Point position, string text)
            : base(position, text, DirectionType.None, SnakesConfig.BOARD_BORDER_FOREGROUND_COLOR, SnakesConfig.BOARD_BORDER_BACKGROUND_COLOR, 0, 0)
        {
            UpdateText();
        }

        public void IncrementMoves()
        {
            Moves++;
            UpdateText();
        }

        public void IncrementAmountEaten()
        {
            AmountEaten++;
            UpdateText();
        }

        private void UpdateText()
        {
            SetText($"| Amount Eaten: {AmountEaten} | Moves: {Moves} | Score: {Total}");
        }
    }
}
