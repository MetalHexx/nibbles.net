using Nibbles.GameObject.Configuration;

namespace Tetris.GameObjects
{
    public class LShapedTetrimino : Tetrimino
    {
        public LShapedTetrimino() : base(GameColor.Red) { }
        protected override int[,] GetRotation(RotationState state)
        {
            switch (state)
            {
                case RotationState.Up:
                    return new int[,]
                    {
                        {0,0,1},
                        {1,1,1},
                        {0,0,0},
                    };

                case RotationState.Right:
                    return new int[,]
                    {
                        {0,1,0},
                        {0,1,0},
                        {0,1,1},
                    };

                case RotationState.Down:
                    return new int[,]
                    {
                        {0,0,0},
                        {1,1,1},
                        {1,0,0},
                    };

                case RotationState.Left:
                    return new int[,]
                    {
                        {1,1,0},
                        {0,1,0},
                        {0,1,0},
                    };
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
