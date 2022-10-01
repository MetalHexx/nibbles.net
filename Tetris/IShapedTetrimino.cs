using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Tetris
{
    public class IShapedTetrimino : Tetrimino
    {
        public IShapedTetrimino() : base(GameColor.Blue) { }
        protected override int[,] GetRotation(RotationState state)
        {
            switch (state)
            {
                case RotationState.Up:
                    return new int[,] 
                    {
                        {0,0,0,0},
                        {1,1,1,1},
                        {0,0,0,0},
                        {0,0,0,0}
                    };
                
                case RotationState.Right:
                    return new int[,] 
                    {
                        {0,0,1,0},
                        {0,0,1,0},
                        {0,0,1,0},
                        {0,0,1,0}
                    };

                case RotationState.Down:
                    return new int[,]
                    {
                        {0,0,0,0},
                        {0,0,0,0},
                        {1,1,1,1},
                        {0,0,0,0}
                    };

                case RotationState.Left:
                    return new int[,]
                    {
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0},
                        {0,1,0,0}
                    };
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
