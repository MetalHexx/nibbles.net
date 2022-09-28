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
    var mainMenu = new MenuManager(renderer, player, new Size(Console.WindowWidth, Console.WindowHeight));
    mainMenu.GameSelected += ExecuteGame;
    new GameLoop(renderer, mainMenu).Start();
}

static void PlaySnakes()
{
    var inputReader = new KeyboardReader();
    var player = new Player(inputReader);
    var gameState = new GameState(player, new Size(Console.WindowWidth, Console.WindowHeight));
    var renderer = new SpriteRenderer();
    var collisionDetector = new CollisionDetector(gameState);
    var scoreStore = new TopScoreStore();
    var snakesGame = new SnakesManager(gameState, renderer, collisionDetector, scoreStore);    
    new GameLoop(renderer, snakesGame).Start();
    Console.ReadLine();
    snakesGame.ShowTopScores();
    
}