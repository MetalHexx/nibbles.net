namespace Tetris
{
    internal class CollisionDetector
    {
        private TetrisState gameState;

        public CollisionDetector(TetrisState gameState)
        {
            this.gameState = gameState;
        }
    }
}