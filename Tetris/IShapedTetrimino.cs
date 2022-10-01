using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Tetris
{
    public class TShapedTetrimino : Tetrimino
    {
        public TShapedTetrimino() : base(GameColor.DarkMagenta) { }
        protected override int[,] GetRotation(RotationState state)
        {
            switch (state)
            {
                case RotationState.Up:
                    return new int[,]
                    {
                        {0,1,0},
                        {1,1,1},
                        {0,0,0},
                    };

                case RotationState.Right:
                    return new int[,]
                    {
                        {0,1,0},
                        {0,1,1},
                        {0,1,0},
                    };

                case RotationState.Down:
                    return new int[,]
                    {
                        {0,0,0},
                        {1,1,1},
                        {0,1,0},
                    };

                case RotationState.Left:
                    return new int[,]
                    {
                        {0,1,0},
                        {1,1,0},
                        {0,1,0},
                    };
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }

    public class ZShapedTetrimino : Tetrimino
    {
        public ZShapedTetrimino() : base(GameColor.Cyan) { }
        protected override int[,] GetRotation(RotationState state)
        {
            switch (state)
            {
                case RotationState.Up:
                    return new int[,]
                    {
                        {1,1,0},
                        {0,1,1},
                        {0,0,0},
                    };

                case RotationState.Right:
                    return new int[,]
                    {
                        {0,0,1},
                        {0,1,1},
                        {0,1,0},
                    };

                case RotationState.Down:
                    return new int[,]
                    {
                        {0,0,0},
                        {1,1,0},
                        {0,1,1},
                    };

                case RotationState.Left:
                    return new int[,]
                    {
                        {0,1,0},
                        {1,1,0},
                        {1,0,0},
                    };
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }

    public class SShapedTetrimino : Tetrimino
    {
        public SShapedTetrimino() : base(GameColor.Gray) { }
        protected override int[,] GetRotation(RotationState state)
        {
            switch (state)
            {
                case RotationState.Up:
                    return new int[,]
                    {
                        {0,1,1},
                        {1,1,0},
                        {0,0,0},
                    };

                case RotationState.Right:
                    return new int[,]
                    {
                        {0,1,0},
                        {0,1,1},
                        {0,0,1},
                    };

                case RotationState.Down:
                    return new int[,]
                    {
                        {0,0,0},
                        {0,1,1},
                        {1,1,0},
                    };

                case RotationState.Left:
                    return new int[,]
                    {
                        {1,0,0},
                        {1,1,0},
                        {0,1,0},
                    };
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }

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

    public class JShapedTetrimino : Tetrimino
    {
        public JShapedTetrimino() : base(GameColor.Yellow) { }
        protected override int[,] GetRotation(RotationState state)
        {
            switch (state)
            {
                case RotationState.Up:
                    return new int[,]
                    {
                        {1,0,0},
                        {1,1,1},
                        {0,0,0},
                    };                

                case RotationState.Right:
                    return new int[,]
                    {
                        {0,1,1},
                        {0,1,0},
                        {0,1,0},
                    };

                case RotationState.Down:
                    return new int[,]
                    {
                        {0,0,0},
                        {1,1,1},
                        {0,0,1},
                    };

                case RotationState.Left:
                    return new int[,]
                    {
                        {0,1,0},
                        {0,1,0},
                        {1,1,0},                        
                    };
                default: throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }

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
