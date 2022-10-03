using Nibbles.GameObject.Configuration;

namespace Tetris.GameObjects
{
    public class OShapedTetrimino : Tetrimino
    {
        public OShapedTetrimino() : base(GameColor.Green) { }
        protected override int[,] GetRotation(RotationState state)
        {
            return new int[,]
            {
                {1,1},
                {1,1}
            };
        }
    }
}
