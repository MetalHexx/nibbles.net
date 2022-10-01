using Nibbles.GameObject.Configuration;

namespace Tetris
{
    public class OShapedTetrimino : Tetrimino
    {
        public OShapedTetrimino() : base(GameColor.Green) { }
        protected override int[,] GetRotation(RotationState state)
        {
            switch (state)
            {
                case RotationState.Up:
                    return new int[,]
                    {
                        {0,1,1},
                        {0,1,1},
                        {0,0,0},
                    };

                case RotationState.Right:
                    return new int[,]
                    {
                        {0,1,1},
                        {0,1,1},
                        {0,0,0},
                    };

                case RotationState.Down:
                    return new int[,]
                    {
                        {0,1,1},
                        {0,1,1},
                        {0,0,0},
                    };

                case RotationState.Left:
                    return new int[,]
                    {
                        {0,1,1},
                        {0,1,1},
                        {0,0,0},
                    };
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
