namespace Nibbles.GameObject
{
    public class Score: GameText
    {
        public int Moves { get; set; }
        public int AmountEaten = 0;

        public int Total
        {
            get { return AmountEaten * _scorePerFeeding - _penaltyPerMove * Moves; }
        }

        private const int _scorePerFeeding = 100;
        private const int _penaltyPerMove = 1;

        public Score(Position position, string text) 
            : base(position, text, SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR) 
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
