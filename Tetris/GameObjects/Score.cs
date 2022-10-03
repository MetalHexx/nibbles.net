using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.UI;
using System.Drawing;

namespace Tetris.GameObjects
{
    public class Score : GameText
{
    public int Moves { get; set; }
    public int LinesCleared = 0;

    public int Total
    {
        get { return LinesCleared * _scorePerLine - _penaltyPerMove * Moves; }
    }

    private const int _scorePerLine = 100;
    private const int _penaltyPerMove = 1;

    public Score(Point position, int zIndex, string text)
        : base(position, zIndex, text, DirectionType.None, TetrisConfig.BOARD_BORDER_FOREGROUND_COLOR, TetrisConfig.BOARD_BORDER_BACKGROUND_COLOR, 0, 0)
    {
        UpdateText();
    }

    public void IncrementMoves()
    {
        Moves++;
        UpdateText();
    }

    public void IncrementLineCleared()
    {
        LinesCleared++;
        UpdateText();
    }

    private void UpdateText()
    {
        SetText($" | Lines Cleared: {LinesCleared} | Moves: {Moves} | Score: {Total}");
    }
}
}