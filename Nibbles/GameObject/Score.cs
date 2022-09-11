using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles.GameObject
{
    public class Score
    {
        public int Moves { get; set; }
        public int AmountEaten = 0;
        public int Total
        {
            get { return AmountEaten * _scorePerFeeding - _penaltyPerMove * Moves; }
        }

        private const int _scorePerFeeding = 100;
        private const int _penaltyPerMove = 1;

        public void IncrementMoves()
        {
            Moves++;
        }

        public void IncrementAmountEaten()
        {
            AmountEaten++;
        }
    }
}
