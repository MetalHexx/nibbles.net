using MainMenu;
using Nibbles.Engine;
using SnakesGame.Engine;
using System.Drawing;

ExecuteMainMenu();
Console.ReadLine();

static void ExecuteGame(string game)
{
    Console.Clear();
    switch (game)
    {
        case GameMenuConfig.SNAKES_TITLE:
            PlaySnakes();
            break;
        default: return;
    }
    ExecuteMainMenu();
}

static void ExecuteMainMenu()
{
    var inputReader = new KeyboardReader();
    var player = new Player(inputReader);
    var renderer = new SpriteRenderer();
    var mainMenu = new MenuStateReducer(renderer, player, new Size(Console.WindowWidth, Console.WindowHeight));
    mainMenu.GameSelected += ExecuteGame;
    new GameMenuLoop(renderer, mainMenu).Start();
}

static void PlaySnakes()
{
    var inputReader = new KeyboardReader();
    var player = new Player(inputReader);
    var gameState = new GameState(player, new Size(Console.WindowWidth, Console.WindowHeight));
    var renderer = new SpriteRenderer();
    var collisionDetector = new CollisionDetector(gameState);
    var scoreStore = new TopScoreStore();
    var soundGenerator = new SoundGenerator();
    var snakesGame = new SnakesStateReducer(gameState, renderer, collisionDetector, scoreStore, soundGenerator);    
    new SnakesGameLoop(renderer, snakesGame).Start();
    Console.ReadLine();
    snakesGame.ShowTopScores();
    
}