using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.UI;
using System.Drawing;

internal class HighScoreReducer : GameStateReducer
{
    private readonly ISpriteRenderer _renderer;
    private readonly IPlayer _player;
    private readonly Board _board;
    private readonly GameText _gameTitle;
    private readonly Menu _menu;

    public HighScoreReducer(ISpriteRenderer renderer, IPlayer player, Size boardSize, IHighScoreStore scoreStore, Guid gameId) : base(renderer)
    {
        _renderer = renderer;
        _player = player;

        _board = new(
                new Point(1, 1),
                new Size(boardSize.Width - 2, boardSize.Height - 1),
                GameConfig.BOARD_ZINDEX);

        _gameTitle = new(
                new Point(3, 1),
                GameConfig.BOARD_TEXT_ZINDEX,
                HighScoreConfig.BOARD_TITLE,
                DirectionType.None,
                HighScoreConfig.BOARD_BORDER_FOREGROUND_COLOR,
                HighScoreConfig.BOARD_BORDER_BACKGROUND_COLOR,
                0, 0);

        var scores = scoreStore.GetScores(gameId)
            .Select(score => $"{score.Initials} {score.Score}");

        var menuXPosition = _board.Size.Width / 2 - 15;
        var menuYPosition = _board.Size.Height / 2 - 5;

        _menu = new(
            new Point(menuXPosition, menuYPosition),
            new Size(25, 15), HighScoreConfig.MENU_ZINDEX,
            HighScoreConfig.MENU_TITLE,
            scores,
            GameColor.Yellow,
            GameColor.DarkMagenta);

        _menu.SpriteCreated += OnSpriteCreated;
        _renderer.Add(_menu);
        _renderer.Add(_board);
        _renderer.Add(_gameTitle);
        _renderer.Add(_board);
    }

    public override void GenerateFrame()
    {
        var playerState = _player.NextState();
        var direction = playerState.GetMove().Direction;

        if(playerState.ActionState == ActionState.Shooting)
        {
            GameOver?.Invoke();
        }
    }

    protected override void HandleGameOver(string text)
    {
        throw new NotImplementedException();
    }

    protected override void HandleGameWin(string text)
    {
        throw new NotImplementedException();
    }
}